using System;
using System.IO;
using System.IO.Compression;
using Funq;
using ServiceStack;
using ServiceStack.Host.AspNet;
using ServiceStack.Host.HttpListener;
using ServiceStack.Web;

namespace HelloWorld
{
    public class AppHost : AppSelfHostBase
    {
        public AppHost()
            : base("Hello World",
                   typeof (HelloService).Assembly)
        {
        }

        public override void Configure(Container container)
        {
            this.PreRequestFilters.Add((request,
                                        response) =>
                                       {
                                           var contentEncoding = request.GetHeader(HttpHeaders.ContentEncoding);

                                           if (string.Equals(contentEncoding,
                                                             CompressionTypes.GZip,
                                                             StringComparison.OrdinalIgnoreCase))
                                           {
                                               this.Decompress(request,
                                                               inputStream =>
                                                               {
                                                                   using (var gZipStream = new GZipStream(inputStream,
                                                                                                          CompressionMode.Decompress))
                                                                   {
                                                                       return new MemoryStream(gZipStream.ToBytes());
                                                                   }
                                                               });
                                           }
                                       });
        }

        public void Decompress(IRequest request,
                               Func<Stream, MemoryStream> decompressFn)
        {
            request.UseBufferedStream = true;

            var decompressedStream = decompressFn.Invoke(request.InputStream);

            var listenerRequest = request as ListenerRequest;
            var aspNetRequest = request as AspNetRequest;
            if (listenerRequest != null)
            {
                listenerRequest.BufferedStream = decompressedStream;
            }
            else if (aspNetRequest != null)
            {
                aspNetRequest.BufferedStream = decompressedStream;
            }
            else
            {
                return;
            }

            request.Headers.Remove(HttpHeaders.ContentEncoding);
        }

        public override object OnAfterExecute(IRequest req,
                                              object requestDto,
                                              object response)
        {
            response = base.OnAfterExecute(req,
                                           requestDto,
                                           response);

            if (response != null &&
                !(response is CompressedResult))
            {
                response = req.ToOptimizedResult(response);
            }

            return response;
        }
    }
}

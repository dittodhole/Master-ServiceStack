using System;
using ServiceStack;
using ServiceStack.Host;
using ServiceStack.Web;

namespace DoeInc.ServiceStack.Extensions
{
    public sealed class RequestIdentifierPlugin : IPlugin
    {
        public const string RequestIdentifierKey = "X-RequestIdentifier";

        public void Register(IAppHost appHost)
        {
            appHost.GlobalRequestFilters.Add(this.InterceptRequest);
            appHost.GlobalResponseFilters.Add(this.InterceptResponse);
        }

        private void InterceptRequest(IRequest request,
                                      IResponse response,
                                      object dto)
        {
            var requestIdentifier = request.GetRequestIdentifier();
            if (string.IsNullOrEmpty(requestIdentifier))
            {
                var hasRequestIdentifier = dto as IHasRequestIdentifier;
                if (hasRequestIdentifier == null)
                {
                    requestIdentifier = this.GenerateRequestIdentifier();
                }
                else
                {
                    requestIdentifier = hasRequestIdentifier.RequestIdentifier;
                    if (string.IsNullOrEmpty(requestIdentifier))
                    {
                        hasRequestIdentifier.RequestIdentifier = requestIdentifier = this.GenerateRequestIdentifier();
                    }
                }

                request.SetItem(RequestIdentifierKey,
                                requestIdentifier);
            }
            else
            {
                var hasRequestIdentifier = dto as IHasRequestIdentifier;
                if (hasRequestIdentifier != null)
                {
                    hasRequestIdentifier.RequestIdentifier = requestIdentifier;
                }
            }
        }

        private void InterceptResponse(IRequest request,
                                       IResponse response,
                                       object dto)
        {
            var requestIdentifier = request.GetRequestIdentifier();

            var hasRequestIdentifier = dto as IHasRequestIdentifier;
            if (hasRequestIdentifier != null)
            {
                hasRequestIdentifier.RequestIdentifier = requestIdentifier;
            }

            response.AddHeader(RequestIdentifierKey,
                               requestIdentifier);
        }

        private string GenerateRequestIdentifier()
        {
            return Guid.NewGuid()
                       .ToString("N");
        }

        public static string GetRequestIdentifier(IRequest request = null)
        {
            if (request == null)
            {
                request = HostContext.GetCurrentRequest();
            }

            return request.GetItemStringValue(RequestIdentifierKey);
        }
    }
}

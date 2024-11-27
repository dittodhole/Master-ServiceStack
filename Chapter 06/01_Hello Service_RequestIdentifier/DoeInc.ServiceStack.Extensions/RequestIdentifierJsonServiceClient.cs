using System.IO;
using ServiceStack;
using ServiceStack.Web;

namespace DoeInc.ServiceStack.Extensions
{
    public class RequestIdentifierJsonServiceClient : JsonServiceClient
    {
        public string RequestIdentifier { get; set; }

        public RequestIdentifierJsonServiceClient()
        {
        }

        public RequestIdentifierJsonServiceClient(string baseUri)
            : base (baseUri)
        {
        }

        public RequestIdentifierJsonServiceClient(string syncReplyBaseUri,
                                                  string asyncOneWayBaseUri)
            : base(syncReplyBaseUri,
                   asyncOneWayBaseUri)
        {
        }

        public override void SerializeToStream(IRequest requestContext,
                                               object request,
                                               Stream stream)
        {
            var hasRequestIdentifier = request as IHasRequestIdentifier;
            if (hasRequestIdentifier != null)
            {
                hasRequestIdentifier.RequestIdentifier = this.RequestIdentifier;
            }

            base.SerializeToStream(requestContext,
                                   request,
                                   stream);
        }
    }
}
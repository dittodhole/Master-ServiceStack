using ServiceStack;
using ServiceStack.Host;
using ServiceStack.Web;

namespace DoeInc.ServiceStack.Extensions
{
    public static class RequestIdentifierExtensions
    {
        public static string GetRequestIdentifier(this IRequest request)
        {
            return request.GetItemStringValue(RequestIdentifierPlugin.RequestIdentifierKey) ?? request.GetParamInRequestHeader(RequestIdentifierPlugin.RequestIdentifierKey);
        }
    }
}
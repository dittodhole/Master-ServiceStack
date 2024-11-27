using ServiceStack;
using ServiceStack.Web;

namespace DoeInc.ServiceStack.Extensions
{
    public static class RequestIdentifierExtensions
    {
        public static string GetRequestIdentifier(this IRequest request)
        {
            return request.GetParamInRequestHeader(RequestIdentifierPlugin.RequestIdentifierKey);
        }
    }
}
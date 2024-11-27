using System;
using ServiceStack;
using ServiceStack.Web;

namespace DoeInc.Ticketing.ServiceInterface
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomAuthorizeAttribute : RequestFilterAttribute
    {
        public override void Execute(IRequest req,
                                     IResponse res,
                                     object requestDto)
        {
            var token = req.GetHeader("X-Token");
            if (string.IsNullOrWhiteSpace(token))
            {
                throw HttpError.Unauthorized("Unauthorized");
            }
        }
    }
}

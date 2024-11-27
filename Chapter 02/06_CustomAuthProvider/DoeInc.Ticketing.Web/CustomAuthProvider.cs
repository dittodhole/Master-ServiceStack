using System.Collections.Generic;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Web;

namespace DoeInc.Ticketing.Web
{
    public class CustomAuthProvider : CredentialsAuthProvider,
                                      IAuthWithRequest
    {
        public new static string Name = "custom";
        public new static string Realm = "/auth/" + CustomAuthProvider.Name;

        public CustomAuthProvider()
        {
            this.Provider = CustomAuthProvider.Name;
            this.AuthRealm = CustomAuthProvider.Realm;
        }

        public void PreAuthenticate(IRequest req,
                                    IResponse res)
        {
            SessionFeature.AddSessionIdToRequestFilter(req,
                                                       res,
                                                       null);
            var customAuth = this.GetCustomAuth(req);
            if (!customAuth.HasValue)
            {
                return;
            }

            var authService = req.TryResolve<AuthenticateService>();
            authService.Request = req; // this is used in the "Authenticate" method
            authService.Post(new Authenticate
                             {
                                 provider = this.Provider,
                                 UserName = customAuth.Value.Key,
                                 Password = customAuth.Value.Value
                             });
        }

        public override object Authenticate(IServiceBase authService,
                                            IAuthSession session,
                                            Authenticate request)
        {
            var httpReq = authService.Request;

            // we need to read the headers again, as the content is not in "request"
            var customAuth = this.GetCustomAuth(httpReq);
            if (!customAuth.HasValue)
            {
                throw HttpError.Unauthorized(ErrorMessages.NotAuthenticated);
            }

            return this.Authenticate(authService,
                                     session,
                                     customAuth.Value.Key,
                                     customAuth.Value.Value,
                                     request.Continue);
        }

        private KeyValuePair<string, string>? GetCustomAuth(IRequest httpReq)
        {
            var userName = httpReq.GetHeader("X-UserName");
            var password = httpReq.GetHeader("X-Password");

            if (string.IsNullOrWhiteSpace(userName) ||
                string.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            var result = new KeyValuePair<string, string>(userName,
                                                          password);

            return result;
        }
    }
}

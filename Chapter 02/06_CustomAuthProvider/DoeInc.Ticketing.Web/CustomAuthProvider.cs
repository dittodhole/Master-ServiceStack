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
            authService.Request = req;
            var response = authService.Post(new Authenticate
                                            {
                                                provider = CustomAuthProvider.Name,
                                                UserName = customAuth.Value.Key,
                                                Password = customAuth.Value.Value
                                            });
        }

        public override object Authenticate(IServiceBase authService,
                                            IAuthSession session,
                                            Authenticate request)
        {
            var httpReq = authService.Request;
            var customAuth = this.GetCustomAuth(httpReq);
            if (!customAuth.HasValue)
            {
                throw HttpError.Unauthorized(ErrorMessages.NotAuthenticated);
            }

            var userName = httpReq.GetHeader("X-userName");
            var password = httpReq.GetHeader("X-password");

            return this.Authenticate(authService,
                                     session,
                                     userName,
                                     password,
                                     request.Continue);
        }

        private KeyValuePair<string, string>? GetCustomAuth(IRequest httpReq)
        {
            var userName = httpReq.GetHeader("X-userName");
            var password = httpReq.GetHeader("X-password");

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

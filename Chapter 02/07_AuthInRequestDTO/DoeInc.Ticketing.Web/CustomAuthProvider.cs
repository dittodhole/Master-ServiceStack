using System.Collections.Generic;
using DoeInc.Ticketing.ServiceModel;
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
            var customAuth = this.GetCustomAuth(res);
            if (!customAuth.HasValue)
            {
                return;
            }

            var authService = req.TryResolve<AuthenticateService>();
            authService.Request = req;
            authService.Post(new Authenticate
                             {
                                 provider = this.Provider,
                                 UserName = customAuth.Value.Key,
                                 Password = customAuth.Value.Value
                             });
        }

        private KeyValuePair<string, string>? GetCustomAuth(IRequest httpReq)
        {
            var hasCredentials = httpReq.Dto as IHasCredentials;
            if (hasCredentials == null)
            {
                return null;
            }

            var userName = hasCredentials.UserName;
            var password = hasCredentials.Password;

            if (string.IsNullOrWhiteSpace(userName) ||
                string.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            return new KeyValuePair<string, string>(userName,
                                                    password);
        }
    }
}

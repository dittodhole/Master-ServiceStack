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

            var hasCredentials = req.Dto as IHasCredentials;
            if (hasCredentials == null)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(hasCredentials.UserName) ||
                string.IsNullOrWhiteSpace(hasCredentials.Password))
            {
                return;
            }

            var authService = req.TryResolve<AuthenticateService>();
            authService.Request = req;
            var response = authService.Post(new Authenticate
                                            {
                                                provider = CustomAuthProvider.Name,
                                                UserName = hasCredentials.UserName,
                                                Password = hasCredentials.Password
                                            });
        }
    }
}

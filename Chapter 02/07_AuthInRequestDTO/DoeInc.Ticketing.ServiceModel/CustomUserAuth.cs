using ServiceStack.Auth;

namespace DoeInc.Ticketing.ServiceModel
{
    public class CustomUserAuth : UserAuth
    {
        public virtual string ImageUrl { get; set; }
    }
}

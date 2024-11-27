using ServiceStack.Auth;

namespace DoeInc.Ticketing.ServiceModel
{
    public class CustomUserAuthDetails : UserAuthDetails
    {
        public virtual string ImageUrl { get; set; }
    }
}

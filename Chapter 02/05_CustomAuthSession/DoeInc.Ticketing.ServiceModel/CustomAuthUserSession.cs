using System.Runtime.Serialization;
using ServiceStack;

namespace DoeInc.Ticketing.ServiceModel
{
    [DataContract]
    public class CustomAuthUserSession : AuthUserSession
    {
        [DataMember]
        public string ImageUrl { get; set; }
    }
}

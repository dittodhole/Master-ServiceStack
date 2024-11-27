using System.Runtime.Serialization;
using ServiceStack;

namespace HelloWorld.Models
{
    [Route("/hello/{Name}")]
    [DataContract]
    public class Hello : IReturn<HelloResponse>
    {
        [DataMember]
        public string Name { get; set; }
    }
}

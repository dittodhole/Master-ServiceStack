using System.Runtime.Serialization;
using ProtoBuf;

namespace HelloWorld.Models
{
    [ProtoContract]
    [DataContract]
    public class HelloResponse
    {
        [ProtoMember(1)]
        [DataMember(Order = 1)]
        public string Result { get; set; }
    }
}

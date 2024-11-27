using DoeInc.ServiceStack.Extensions;

namespace HelloWorld
{
    public class HelloResponse : IHasRequestIdentifier
    {
        public string Result { get; set; }
        public string RequestIdentifier { get; set; }
    }
}

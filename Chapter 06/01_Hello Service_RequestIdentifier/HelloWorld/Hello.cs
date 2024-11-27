using DoeInc.ServiceStack.Extensions;
using ServiceStack;

namespace HelloWorld
{
    [Route("/hello/{Name}")]
    public class Hello : IReturn<HelloResponse>,
                         IHasRequestIdentifier
    {
        public string Name { get; set; }
        public Volume Volume { get; set; }
        public string RequestIdentifier { get; set; }
    }
}

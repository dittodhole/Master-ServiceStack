using DoeInc.ServiceStack.Extensions;
using ServiceStack;

namespace HelloWorld
{
    [Route("/hello/{Name}")]
    [ThrottleRestriction(PerMinute = 10)]
    public class Hello : IReturn<HelloResponse>
    {
        public string Name { get; set; }
        public Volume Volume { get; set; }
    }
}

using System;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace HelloWorld
{
    [Route("/hello/{Name}")]
    [Api("foobar")]
    public class Hello : IReturn<HelloResponse>
    {
        [Obsolete]
        public string Name { get; set; }
        public Volume Volume { get; set; }
    }
}

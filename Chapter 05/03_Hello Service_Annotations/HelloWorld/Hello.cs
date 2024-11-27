using ServiceStack;
using ServiceStack.DataAnnotations;

namespace HelloWorld
{
    [Route("/hello/{Name}")]
    [Exclude(Feature.Metadata)]
    [Restrict(VisibleLocalhostOnly = true)]
    public class Hello : IReturn<HelloResponse>
    {
        public string Name { get; set; }
        public Volume Volume { get; set; }
    }
}

using ServiceStack;

namespace HelloWorld.Website
{
    [Route("/hello/{Name}")]
    public class Hello : IReturn<HelloResponse>
    {
        public string Name { get; set; }
    }
}

using ServiceStack;

namespace HelloWorld.Website
{
    [Route("/hello/{Name}")]
    public class Hello : IReturn<string>
    {
        public string Name { get; set; }
    }
}

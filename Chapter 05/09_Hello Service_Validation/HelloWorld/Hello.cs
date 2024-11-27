using ServiceStack;

namespace HelloWorld
{
    [Route("/hello/{Name}")]
    public class Hello : IReturn<HelloResponse>
    {
        public string Name { get; set; }
        public Volume Volume { get; set; }
    }
}

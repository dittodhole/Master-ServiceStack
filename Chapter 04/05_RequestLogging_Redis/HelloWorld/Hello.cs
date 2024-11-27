using ServiceStack;

namespace HelloWorld
{
    [Route("/hello/{Name}")]
    public class Hello : IReturn<string>
    {
        public string Name { get; set; }
    }
}

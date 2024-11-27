using ServiceStack;

namespace HelloWorld
{
    [Route("/hello/{Name}")]
    [Route("/hello")]
    public class Hello : IReturn<HelloResponse>,
                         IHasVersion
    {
        public string Name { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public Volume Volume { get; set; }
        public int Version { get; set; }
    }
}

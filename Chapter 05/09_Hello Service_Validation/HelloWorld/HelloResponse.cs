using ServiceStack;

namespace HelloWorld
{
    public class HelloResponse : IHasResponseStatus
    {
        public string Result { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }
}

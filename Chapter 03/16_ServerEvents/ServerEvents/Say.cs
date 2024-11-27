using ServiceStack;

namespace ServerEvents
{
    [Route("/say/{Message}")]
    public class Say : IReturnVoid
    {
        public string Message { get; set; }
    }
}

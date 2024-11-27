using ServiceStack;

namespace ServerEvents
{
    public class Say : IReturnVoid
    {
        public string Message { get; set; }
    }
}

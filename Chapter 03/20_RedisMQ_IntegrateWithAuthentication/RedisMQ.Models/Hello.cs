using ServiceStack;

namespace RedisMQ.Models
{
    public sealed class Hello : IReturnVoid
    {
        public string SessionId { get; set; }
    }
}

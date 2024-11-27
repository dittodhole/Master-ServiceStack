using ServiceStack;

namespace RedisMQ.Models
{
    public sealed class Hello : IReturnVoid,
                                IHasSessionId
    {
        public string SessionId { get; set; }
    }
}

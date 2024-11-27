using ServiceStack;

namespace RedisMQ.Models
{
    public sealed class Hello : IReturn<HelloResponse>
    {
        public string Name { get; set; }
    }
}

using ServiceStack;

namespace RabbitMQ.Models
{
    public class Hello : IReturn<HelloResponse>
    {
        public string Name { get; set; }
    }
}

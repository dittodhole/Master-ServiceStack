using RabbitMQ.Models;
using ServiceStack;
using ServiceStack.RabbitMq;

namespace RabbitMQ.ComponentA
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var rabbitMqServer = new RabbitMqServer();
            var messageProducer = rabbitMqServer.CreateMessageProducer();

            for (var i = 0;
                 i < 10;
                 i++)
            {
                var hello = new Hello
                            {
                                Name = i.ToString()
                            };
                messageProducer.Publish(hello);
            }

            messageProducer.Dispose();
            rabbitMqServer.Dispose();
        }
    }
}

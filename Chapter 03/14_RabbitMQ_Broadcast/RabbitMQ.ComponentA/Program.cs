using RabbitMQ.Client;
using RabbitMQ.Models;
using ServiceStack;
using ServiceStack.Messaging;
using ServiceStack.RabbitMq;

namespace RabbitMQ.ComponentA
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var rabbitMqServer = new RabbitMqServer();
            var messageProducer = (RabbitMqProducer) rabbitMqServer.CreateMessageProducer();
            var channel = messageProducer.Channel;
            channel.ExchangeDeclare(CustomExchangeNames.FanoutExchangeName,
                                    ExchangeType.Fanout,
                                    durable: true,
                                    autoDelete: false,
                                    arguments: null);

            var hello = new Hello
                        {
                            Name = "Demo"
                        };
            var message = new Message<Hello>(hello);
            messageProducer.Publish(QueueNames<Hello>.In,
                                    message,
                                    CustomExchangeNames.FanoutExchangeName);

            messageProducer.Dispose();
            rabbitMqServer.Dispose();
        }
    }
}

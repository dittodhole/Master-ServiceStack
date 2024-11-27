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
            channel.RegisterFanoutExchange(CustomExchangeNames.FanoutExchangeName);

            var hello = new Hello
                        {
                            Name = "Demo"
                        };
            var message = new Message<Hello>(hello);
            messageProducer.Publish(queueName: QueueNames<Hello>.In, // routingKey
                                    message: message,
                                    exchange: CustomExchangeNames.FanoutExchangeName);

            messageProducer.Dispose();
            rabbitMqServer.Dispose();
        }
    }
}

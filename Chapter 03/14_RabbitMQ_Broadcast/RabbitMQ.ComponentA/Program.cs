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
            channel.ExchangeDeclare(exchange: CustomExchangeNames.FanoutExchangeName,
                                    type: ExchangeType.Fanout,
                                    durable: true,
                                    autoDelete: false,
                                    arguments: null);

            var hello = new Hello
                        {
                            Name = "Demo"
                        };
            var message = new Message<Hello>(hello);
            messageProducer.Publish(queueName: QueueNames<Hello>.In,
                                    message: message,
                                    exchange: CustomExchangeNames.FanoutExchangeName);

            messageProducer.Dispose();
            rabbitMqServer.Dispose();
        }
    }
}

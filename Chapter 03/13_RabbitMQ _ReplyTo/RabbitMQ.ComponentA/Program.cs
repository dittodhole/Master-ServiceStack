using System;
using RabbitMQ.Models;
using ServiceStack;
using ServiceStack.Messaging;
using ServiceStack.RabbitMq;
using ServiceStack.Text;

namespace RabbitMQ.ComponentA
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var rabbitMqServer = new RabbitMqServer();
            var messageQueueClient = rabbitMqServer.CreateMessageQueueClient();

            var queueName = messageQueueClient.GetTempQueueName();
            var hello = new Hello
                        {
                            Name = "reply to originator"
                        };
            messageQueueClient.Publish(new Message<Hello>(hello)
                                       {
                                           ReplyTo = queueName
                                       });
            var message = messageQueueClient.Get<HelloResponse>(queueName);
            var helloResponse = message.GetBody();
            helloResponse.Result.Print();

            Console.ReadLine();

            messageQueueClient.Dispose();
            rabbitMqServer.Dispose();
        }
    }
}

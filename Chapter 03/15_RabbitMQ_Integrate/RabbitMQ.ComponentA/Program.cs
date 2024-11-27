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
            var licensePath = @"~/../../../../license.txt".MapHostAbsolutePath();
            Licensing.RegisterLicenseFromFileIfExists(licensePath);

            var rabbitMqServer = new RabbitMqServer();
            var messageQueueClient = rabbitMqServer.CreateMessageQueueClient();

            var queueName = messageQueueClient.GetTempQueueName();
            var hello = new Hello
                        {
                            Name = "Demo"
                        };
            var message = new Message<Hello>(hello)
                          {
                              ReplyTo = queueName
                          };
            messageQueueClient.Publish(message);
            var response = messageQueueClient.Get<HelloResponse>(queueName);
            var helloResponse = response.GetBody();
            helloResponse.Result.Print();

            messageQueueClient.Dispose();
            rabbitMqServer.Dispose();
        }
    }
}

using RabbitMQ.Models;
using ServiceStack;
using ServiceStack.RabbitMq;

namespace RabbitMQ.ComponentA
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var licensePath = @"~/../../../../license.txt".MapHostAbsolutePath();
            Licensing.RegisterLicenseFromFileIfExists(licensePath);

            var rabbitMqServer = new RabbitMqServer();
            var messageProducer = rabbitMqServer.CreateMessageProducer();

            var hello = new Hello
            {
                Name = "Demo"
            };
            messageProducer.Publish(hello);

            messageProducer.Dispose();
            rabbitMqServer.Dispose();
        }
    }
}

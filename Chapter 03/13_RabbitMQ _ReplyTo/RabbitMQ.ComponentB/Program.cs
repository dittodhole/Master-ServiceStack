using System;
using RabbitMQ.Models;
using ServiceStack;
using ServiceStack.RabbitMq;
using ServiceStack.Text;

namespace RabbitMQ.ComponentB
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var licensePath = @"~/../../../../license.txt".MapHostAbsolutePath();
            Licensing.RegisterLicenseFromFileIfExists(licensePath);

            var rabbitMqServer = new RabbitMqServer();
            rabbitMqServer.RegisterHandler<Hello>(message =>
                                                  {
                                                      var hello = message.GetBody();
                                                      var name = hello.Name;
                                                      var helloResponse = new HelloResponse
                                                                          {
                                                                              Result = "Hello {0}".Fmt(name)
                                                                          };

                                                      return helloResponse;
                                                  });
            rabbitMqServer.Start();

            "listening for hello messages and replying a helloResponse back".Print();

            Console.ReadLine();

            rabbitMqServer.Dispose();
        }
    }
}

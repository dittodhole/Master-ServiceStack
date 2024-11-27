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
            var rabbitMqServer = new RabbitMqServer();
            rabbitMqServer.RegisterHandler<Hello>(message =>
                                                  {
                                                      var hello = message.GetBody();
                                                      var name = hello.Name;
                                                      var result = "Hello {0}".Fmt(name);

                                                      result.Print();

                                                      return null;
                                                  });
            rabbitMqServer.Start();

            "listening for hello messages".Print();

            Console.ReadLine();

            rabbitMqServer.Dispose();
        }
    }
}

using System;
using ServiceStack;
using ServiceStack.RabbitMq;
using ServiceStack.Text;

namespace LogReceiver
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var rabbitMqServer = new RabbitMqServer())
            {
                rabbitMqServer.RegisterHandler<RequestLogEntry>(message =>
                                                                {
                                                                    var requestLogEntry = message.GetBody();

                                                                    requestLogEntry.PrintDump();

                                                                    return null;
                                                                });
                rabbitMqServer.Start();

                "Press Enter to exit".Print();

                Console.ReadLine();
            }
        }
    }
}

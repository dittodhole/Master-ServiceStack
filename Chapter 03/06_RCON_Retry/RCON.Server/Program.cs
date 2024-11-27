using System;
using System.Net;
using RCON.Models;
using ServiceStack;
using ServiceStack.Text;

namespace RCON.Server
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const int port = 12345;
            var ipEndPoint = new IPEndPoint(IPAddress.Any,
                                            port);
            var messageService = new ServiceStack.Messaging.Rcon.Server(ipEndPoint);
            messageService.RegisterHandler<Hello>(message =>
                                                  {
                                                      var hello = message.GetBody();
                                                      var name = hello.Name;

                                                      "incoming request {0}; attempt {1}".Print(name,
                                                                                                message.RetryAttempts);

                                                      if (message.RetryAttempts == 0)
                                                      {
                                                          "failing {0} on attempt {1}".Print(name,
                                                                                             message.RetryAttempts);
                                                          throw new Exception();
                                                      }

                                                      var helloResponse = new HelloResponse
                                                                          {
                                                                              Result = "Hello {0}".Fmt(name)
                                                                          };

                                                      return helloResponse;
                                                  });
            messageService.Start();

            "RCON server is running, press ENTER to exit".Print();

            Console.ReadLine();

            messageService.Dispose();
        }
    }
}

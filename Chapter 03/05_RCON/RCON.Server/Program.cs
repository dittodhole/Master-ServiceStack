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
            var licensePath = @"~/../../../../license.txt".MapHostAbsolutePath();
            Licensing.RegisterLicenseFromFileIfExists(licensePath);

            const int port = 12345;
            var ipEndPoint = new IPEndPoint(IPAddress.Any,
                                            port);
            var messageService = new ServiceStack.Messaging.Rcon.Server(ipEndPoint);
            messageService.RegisterHandler<Hello>(message =>
                                                  {
                                                      var hello = message.GetBody();
                                                      var name = hello.Name;
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

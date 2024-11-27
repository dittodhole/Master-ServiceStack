using System;
using System.Net;
using ServiceStack;
using ServiceStack.Messaging.Rcon;
using ServiceStack.Text;

namespace RCON
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var ipAddress = IPAddress.Parse("127.0.0.1");
            var ipEndPoint = new IPEndPoint(ipAddress,
                                            12345);
            var messageService = new Server(ipEndPoint);
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

            var client = new Client(ipEndPoint);
            client.Connect();

            for (var i = 0;
                 i < 10;
                 i++)
            {
                var hello = new Hello
                            {
                                Name = i.ToString()
                            };

                client.Call(hello,
                            (rcon,
                             response) =>
                            {
                                var message = response.ToMessage<HelloResponse>();
                                var helloResponse = message.GetBody();
                                helloResponse.Result.Print();
                            });
            }

            Console.ReadLine();

            client.Disconnect();
            messageService.Stop();
        }

        public class Hello : IReturn<HelloResponse>
        {
            public string Name { get; set; }
        }

        public class HelloResponse
        {
            public string Result { get; set; }
        }
    }
}

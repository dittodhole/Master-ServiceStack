using System;
using System.Net;
using System.Threading.Tasks;
using RCON.Models;
using ServiceStack;
using ServiceStack.Text;

namespace RCON.Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Parallel.For(0,
                         2,
                         clientCounter =>
                         {
                             var ipAddress = IPAddress.Parse("127.0.0.1");
                             var ipEndPoint = new IPEndPoint(ipAddress,
                                                             12345);
                             var client = new ServiceStack.Messaging.Rcon.Client(ipEndPoint);
                             client.Connect();

                             for (var i = 0;
                                  i < 10;
                                  i++)
                             {
                                 var hello = new Hello
                                             {
                                                 Name = "client: {0}, {1}".Fmt(clientCounter,
                                                                               i)
                                             };
                                 client.Call(hello,
                                             (rcon,
                                              response) =>
                                             {
                                                 var message = response.ToMessage<HelloResponse>();
                                                 var helloResponse = message.GetBody();
                                                 "reply for client {0}: {1}".Print(clientCounter,
                                                                                   helloResponse.Result);
                                             });
                             }
                         });
            Console.ReadLine();
        }
    }
}

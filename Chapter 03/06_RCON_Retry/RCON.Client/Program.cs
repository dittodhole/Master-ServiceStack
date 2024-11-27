using System;
using System.Net;
using RCON.Models;
using ServiceStack;
using ServiceStack.Text;

namespace RCON.Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var licensePath = @"~/../../../../license.txt".MapHostAbsolutePath();
            Licensing.RegisterLicenseFromFileIfExists(licensePath);

            const int port = 12345;
            var ipEndPoint = new IPEndPoint(IPAddress.Loopback,
                                            port);
            var client = new Client(ipEndPoint);
            client.Connect();

            var hello = new Hello
            {
                Name = i.ToString()
            };
            client.Call(hello,
                        Program.CallbackAfterHello);
            Console.ReadLine();
        }

        private static void CallbackAfterHello(ServiceStack.Messaging.Rcon.Client rconClient,
                                               byte[] response)
        {
            var initialMessage = response.ToMessage<Hello>();
            if (initialMessage.Error != null)
            {
                var client = (Client) rconClient;
                client.Requeue(initialMessage,
                               Program.CallbackAfterHello);
            }
            else
            {
                var message = response.ToMessage<HelloResponse>();
                var helloResponse = message.GetBody();
                helloResponse.Result.Print();
            }
        }
    }
}

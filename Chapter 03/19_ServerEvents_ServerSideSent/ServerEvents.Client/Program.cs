using System;
using ServiceStack;
using ServiceStack.Text;

namespace ServerEvents.Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var licensePath = @"~/../../../../license.txt".MapHostAbsolutePath();
            Licensing.RegisterLicenseFromFileIfExists(licensePath);

            var serverEventsClient = new ServerEventsClient("http://localhost:54392");
            serverEventsClient.RegisterReceiver<CustomReceiver>();
            serverEventsClient.Connect();

            serverEventsClient.ServiceClient.Send(new Say
                                                  {
                                                      Message = "hello there"
                                                  });

            "listening to the server".Print();

            Console.ReadLine();
        }
    }
}

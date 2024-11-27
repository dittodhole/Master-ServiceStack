using System;
using ServiceStack;
using ServiceStack.Text;

namespace ServerEvents.Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var serverEventsClient = new ServerEventsClient("http://localhost:54392");
            serverEventsClient.OnMessage = serverEventMessage =>
                                           {
                                               serverEventMessage.PrintDump();
                                           };
            serverEventsClient.OnCommand = serverEventMessage =>
                                           {
                                               serverEventMessage.PrintDump();
                                           };
            serverEventsClient.OnConnect = serverEventMessage =>
                                           {
                                               serverEventMessage.PrintDump();
                                           };
            serverEventsClient.OnException = exception =>
                                             {
                                                 exception.PrintDump();
                                             };
            serverEventsClient.OnHeartbeat = () =>
                                             {
                                                 "heartbeat".Print();
                                             };
            serverEventsClient.Connect();

            "listening to the server".Print();

            Console.ReadLine();
        }
    }
}

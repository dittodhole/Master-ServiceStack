using System;
using System.Collections.Generic;
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
                                               "OnMessage: {0}".Print(serverEventMessage);
                                           };
            serverEventsClient.OnCommand = serverEventMessage =>
                                           {
                                               "OnCommand: {0}".Print(serverEventMessage);
                                           };
            serverEventsClient.OnConnect = serverEventConnect =>
                                           {
                                               "OnConnect: {0}".Print(serverEventConnect);
                                           };
            serverEventsClient.OnException = exception =>
                                             {
                                                 "OnException: {0}".Print(exception);
                                             };
            serverEventsClient.OnHeartbeat = () =>
                                             {
                                                 "heartbeat".Print();
                                             };
            serverEventsClient.Connect();

            serverEventsClient.RegisterHandlers(new Dictionary<string, ServerEventCallback>
                                                {
                                                    {
                                                        "Say", (source,
                                                                message) =>
                                                               {
                                                                   var say = message.Json.FromJson<Say>();
                                                                   say.PrintDump();
                                                               }
                                                    }
                                                });
            serverEventsClient.RegisterReceiver<CustomReceiver>();

            "listening to the server".Print();

            Console.ReadLine();
        }
    }
}

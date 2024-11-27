using System;
using ServiceStack;
using ServiceStack.Messaging.Redis;
using ServiceStack.Redis;
using ServiceStack.Text;

namespace LogReceiver
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var redisClientsManager = new PooledRedisClientManager())
            {
                using (var messageService = new RedisMqServer(redisClientsManager))
                {
                    messageService.RegisterHandler<RequestLogEntry>(message =>
                                                                    {
                                                                        var requestLogEntry = message.GetBody();

                                                                        requestLogEntry.PrintDump();

                                                                        return null;
                                                                    });
                    messageService.Start();

                    "Press Enter to exit".Print();

                    Console.ReadLine();
                }
            }
        }
    }
}

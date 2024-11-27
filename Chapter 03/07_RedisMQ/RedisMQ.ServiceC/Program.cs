using System;
using RedisMQ.Models;
using ServiceStack.Messaging.Redis;
using ServiceStack.Redis;
using ServiceStack.Text;

namespace RedisMQ.ServiceC
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var pooledRedisClientManager = new BasicRedisClientManager();
            var redisMqServer = new RedisMqServer(pooledRedisClientManager);

            redisMqServer.RegisterHandler<HelloResponse>(message =>
                                                         {
                                                             var helloResponse = message.GetBody();
                                                             var result = helloResponse.Result;
                                                             result.Print();

                                                             return null;
                                                         });

            redisMqServer.Start();

            "listening for helloResponses, which get printed".Print();

            Console.ReadLine();
        }
    }
}

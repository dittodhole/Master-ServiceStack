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
            var licensePath = @"~/../../../../license.txt".MapHostAbsolutePath();
            Licensing.RegisterLicenseFromFileIfExists(licensePath);

            var redisClientManager = new BasicRedisClientManager();
            var redisMqServer = new RedisMqServer(redisClientManager);

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

            redisMqServer.Dispose();
            redisClientManager.Dispose();
        }
    }
}

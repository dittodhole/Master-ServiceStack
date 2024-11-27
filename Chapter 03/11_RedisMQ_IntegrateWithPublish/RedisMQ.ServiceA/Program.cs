using System;
using RedisMQ.Models;
using ServiceStack.Messaging.Redis;
using ServiceStack.Redis;
using ServiceStack.Text;

namespace RedisMQ.ServiceA
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

            Console.ReadLine();

            redisMqServer.Stop();
            redisClientManager.Dispose();
        }
    }
}

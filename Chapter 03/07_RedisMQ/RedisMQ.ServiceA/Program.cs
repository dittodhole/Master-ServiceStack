using System;
using RedisMQ.Models;
using ServiceStack;
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
            var messageProducer = redisMqServer.CreateMessageProducer();

            for (var i = 0;
                 i < 10;
                 i++)
            {
                var hello = new Hello
                            {
                                Name = i.ToString()
                            };
                messageProducer.Publish(hello);
                "sent name {0} to Redis".Print(i);
            }

            Console.ReadLine();

            messageProducer.Dispose();
            redisMqServer.Dispose();
            redisClientManager.Dispose();
        }
    }
}

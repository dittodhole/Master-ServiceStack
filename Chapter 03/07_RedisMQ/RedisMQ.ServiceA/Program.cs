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
            var pooledRedisClientManager = new BasicRedisClientManager();
            var redisMqServer = new RedisMqServer(pooledRedisClientManager);
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
        }
    }
}

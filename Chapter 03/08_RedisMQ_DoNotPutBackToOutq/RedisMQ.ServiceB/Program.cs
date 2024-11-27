using System;
using RedisMQ.Models;
using ServiceStack;
using ServiceStack.Messaging;
using ServiceStack.Messaging.Redis;
using ServiceStack.Redis;
using ServiceStack.Text;

namespace RedisMQ.ServiceB
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var redisClientManager = new BasicRedisClientManager();
            var redisMqServer = new RedisMqServer(redisClientManager);
            redisMqServer.RegisterHandler<Hello>(message =>
                                                 {
                                                     message.Options = (int) MessageOption.None;

                                                     var hello = message.GetBody();
                                                     var name = hello.Name;
                                                     name.Print();

                                                     return null;
                                                 });
            redisMqServer.Start();

            Console.ReadLine();

            redisMqServer.Dispose();
            redisClientManager.Dispose();
        }
    }
}

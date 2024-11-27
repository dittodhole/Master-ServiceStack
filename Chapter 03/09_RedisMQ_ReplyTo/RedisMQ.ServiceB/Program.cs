using System;
using RedisMQ.Models;
using ServiceStack;
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
                                                     var hello = message.GetBody();
                                                     var name = hello.Name;
                                                     var helloResponse = new HelloResponse
                                                                         {
                                                                             Result = "Hello {0}".Fmt(name)
                                                                         };

                                                     return helloResponse;
                                                 });
            redisMqServer.Start();

            "listening for hello's, which get forwarded as helloResponses".Print();

            Console.ReadLine();

            redisMqServer.Dispose();
            redisClientManager.Dispose();
        }
    }
}

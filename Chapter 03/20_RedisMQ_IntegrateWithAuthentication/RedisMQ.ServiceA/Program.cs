using System;
using RedisMQ.Models;
using ServiceStack;
using ServiceStack.Messaging;
using ServiceStack.Messaging.Redis;
using ServiceStack.Redis;
using ServiceStack.Text;

namespace RedisMQ.ServiceA
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            AuthenticateResponse authenticateResponse;
            using (var jsonServiceClient = new JsonServiceClient(Constants.UrlBase))
            {
                authenticateResponse = jsonServiceClient.Post(new Authenticate
                                                              {
                                                                  RememberMe = true,
                                                                  UserName = "johndoe",
                                                                  Password = "password"
                                                              });
            }

            var redisClientManager = new BasicRedisClientManager();
            var redisMqServer = new RedisMqServer(redisClientManager);
            var messageQueueClient = redisMqServer.CreateMessageQueueClient();

            var queueName = messageQueueClient.GetTempQueueName();
            var hello = new Hello
                        {
                            Name = "demo",
                            SessionId = authenticateResponse.SessionId
                        };
            var message = new Message<Hello>(hello)
                          {
                              ReplyTo = queueName
                          };
            messageQueueClient.Publish(message);
            var response = messageQueueClient.Get<HelloResponse>(queueName);
            var helloResponse = response.GetBody();
            helloResponse.Result.Print();

            Console.ReadLine();

            messageQueueClient.Dispose();
            redisMqServer.Dispose();
            redisClientManager.Dispose();
        }
    }
}

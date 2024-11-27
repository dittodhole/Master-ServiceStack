using Funq;
using RedisMQ.Models;
using ServiceStack;
using ServiceStack.Messaging.Redis;
using ServiceStack.Redis;
using ServiceStack.Text;

namespace RedisMQ.ServiceB
{
    public class AppHost : AppSelfHostBase
    {
        public AppHost()
            : base("Hello Service",
                   typeof (Service).Assembly)
        {
        }

        public override void Configure(Container container)
        {
            this.Routes.Add<Hello>("/hello");
            this.Routes.Add<Hello>("/hello/{Name}");

            this.RegisterTypedMessageRequestFilter<Hello>((request,
                                                           response,
                                                           dto) =>
                                                          {
                                                              "request coming in".Print();
                                                          });
            this.RegisterTypedMessageResponseFilter<HelloResponse>((request,
                                                                    response,
                                                                    dto) =>
                                                                   {
                                                                       "response going out".Print();
                                                                   });

            container.Register<IRedisClientsManager>(arg => new RedisManagerPool());
            var redisClientsManager = container.Resolve<IRedisClientsManager>();
            var redisMqServer = new RedisMqServer(redisClientsManager);
            redisMqServer.RegisterHandler<Hello>(this.ServiceController.ExecuteMessage);
            redisMqServer.Start();
        }
    }
}

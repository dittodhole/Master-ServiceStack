using Funq;
using ServiceStack;
using ServiceStack.Host;
using ServiceStack.Redis;

namespace HelloWorld
{
    public class AppHost : AppSelfHostBase
    {
        public AppHost()
            : base("Hello World",
                   typeof (HelloService).Assembly)
        {
        }

        public override void Configure(Container container)
        {
            container.Register<IRedisClientsManager>(arg => new PooledRedisClientManager());

            this.Plugins.Add(new RequestLogsFeature
                             {
                                 RequestLogger = new RedisRequestLogger(container.Resolve<IRedisClientsManager>())
                             });
        }
    }
}

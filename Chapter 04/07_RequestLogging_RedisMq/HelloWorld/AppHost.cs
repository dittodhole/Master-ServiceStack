using Funq;
using ServiceStack;
using ServiceStack.Messaging;
using ServiceStack.Messaging.Redis;
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
            container.Register<IMessageService>(arg => new RedisMqServer(arg.Resolve<IRedisClientsManager>()));

            this.Plugins.Add(new RequestLogsFeature
                             {
                                 RequestLogger = new MessageServiceRequestLogger()
                             });
        }
    }
}

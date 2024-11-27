using Funq;
using ServiceStack;
using ServiceStack.Messaging;
using ServiceStack.MiniProfiler;
using ServiceStack.MiniProfiler.Storage;
using ServiceStack.RabbitMq;

namespace HelloWorld.Website
{
    public class AppHost : AppHostBase
    {
        public AppHost()
            : base("Hello World",
                   typeof (HelloService).Assembly)
        {
        }

        public override void Configure(Container container)
        {
            container.Register<IMessageService>(arg => new RabbitMqServer());
            container.RegisterAutoWiredAs<MessageServiceStorage, IStorage>();

            Profiler.Settings.Storage = container.Resolve<IStorage>();
        }
    }
}

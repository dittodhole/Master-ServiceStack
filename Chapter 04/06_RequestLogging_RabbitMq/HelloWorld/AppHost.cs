using Funq;
using ServiceStack;
using ServiceStack.Messaging;
using ServiceStack.RabbitMq;

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
            container.Register<IMessageService>(arg => new RabbitMqServer());

            this.Plugins.Add(new RequestLogsFeature
                             {
                                 RequestLogger = new MessageServiceRequestLogger()
                             });
        }
    }
}

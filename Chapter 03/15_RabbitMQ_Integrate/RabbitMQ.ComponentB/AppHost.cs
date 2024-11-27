using Funq;
using RabbitMQ.Models;
using ServiceStack;
using ServiceStack.Messaging;
using ServiceStack.RabbitMq;

namespace RabbitMQ.ComponentB
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

            container.Register<IMessageService>(arg => new RabbitMqServer());
            var messageService = container.Resolve<IMessageService>();
            messageService.RegisterHandler<Hello>(this.ServiceController.ExecuteMessage);
            messageService.Start();
        }
    }
}

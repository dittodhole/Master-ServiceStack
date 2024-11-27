using Funq;
using ServiceStack;
using ServiceStack.Validation;

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
            this.Plugins.Add(new ValidationFeature());
            container.RegisterValidators(typeof (AppHost).Assembly);
        }
    }
}

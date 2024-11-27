using Funq;
using ServiceStack;

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
            this.SetConfig(new HostConfig
                           {
                               EnableFeatures = Feature.All.Remove(Feature.Metadata)
                           });
        }
    }
}

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
            this.Plugins.Add(new RequestLogsFeature
                             {
                                 EnableRequestBodyTracking = true
                             });
        }
    }
}

using System.Web.Mvc;
using Funq;
using ServiceStack;
using ServiceStack.Mvc;

namespace HelloWorld.Website.api
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
            ControllerBuilder.Current.SetControllerFactory(new FunqControllerFactory(container));
            this.Plugins.Add(new RequestLogsFeature
                             {
                                 EnableRequestBodyTracking = true,
                                 EnableResponseTracking = true,
                                 EnableSessionTracking = true
                             });
        }
    }
}

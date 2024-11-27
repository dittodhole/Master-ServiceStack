using Funq;
using ServiceStack;
using ServiceStack.Razor;

namespace ServerEvents
{
    public class AppHost : AppHostBase
    {
        public AppHost()
            : base("Hello Service",
                   typeof (MessageService).Assembly)
        {
        }

        public override void Configure(Container container)
        {
            this.Plugins.Add(new RazorFormat());
            this.Plugins.Add(new ServerEventsFeature());
            this.Plugins.Add(new CorsFeature());
        }
    }
}

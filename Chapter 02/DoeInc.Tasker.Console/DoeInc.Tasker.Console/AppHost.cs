using Funq;
using ServiceStack;

namespace DoeInc.Tasker.Console
{
    public sealed class AppHost : AppSelfHostBase
    {
        public AppHost()
            : base("Tasker Service",
                   typeof (AppHost).Assembly)
        {
        }

        public override void Configure(Container container)
        {
            this.Plugins.Add(new SessionFeature());
        }
    }
}

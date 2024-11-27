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
            // this will only force the generation and response of a session-id and permanent session-id
            this.Plugins.Add(new SessionFeature());
        }
    }
}

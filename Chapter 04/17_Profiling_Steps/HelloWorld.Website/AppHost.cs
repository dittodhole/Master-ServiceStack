using Funq;
using ServiceStack;

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
        }
    }
}

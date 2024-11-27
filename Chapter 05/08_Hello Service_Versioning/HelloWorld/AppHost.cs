using System.Text;
using System.Xml;
using Funq;
using ServiceStack;
using ServiceStack.Text;

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
            JsConfig.ModelFactory = type => () =>
                                            {
                                                var instance = type.CreateInstance();
                                                var iHasVersion = instance as IHasVersion;
                                                if (iHasVersion != null)
                                                {
                                                    iHasVersion.Version = 0;
                                                }
                                                return instance;
                                            };
        }
    }
}

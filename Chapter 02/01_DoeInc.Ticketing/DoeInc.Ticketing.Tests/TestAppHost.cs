using System;
using System.Reflection;
using Funq;
using ServiceStack;

namespace DoeInc.Ticketing.Tests
{
    public class TestAppHost : AppSelfHostBase
    {
        public TestAppHost(params Assembly[] assembliesWithServices)
            : base("Test App Host",
                   assembliesWithServices)
        {
        }

        public Action<Container> ConfigureContainer { get; set; }

        public override void Configure(Container container)
        {
            if (this.ConfigureContainer != null)
            {
                this.ConfigureContainer.Invoke(container);
            }
        }
    }
}

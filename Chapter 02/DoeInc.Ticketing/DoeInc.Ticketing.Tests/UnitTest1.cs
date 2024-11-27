using DoeInc.Ticketing.ServiceInterface;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.Testing;

namespace DoeInc.Ticketing.Tests
{
    [TestFixture]
    public class UnitTests
    {
        private readonly ServiceStackHost _appHost;

        public UnitTests()
        {
            this._appHost = new BasicAppHost(typeof (TicketService).Assembly)
                            {
                                ConfigureContainer = container =>
                                                     {
                                                         //Add your IoC dependencies here
                                                     }
                            }.Init();
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            this._appHost.Dispose();
        }

        [Test]
        public void TestMethod1()
        {
            /*
            var service = appHost.Container.Resolve<MyServices>();

            var response = (HelloResponse)service.Any(new Hello { Name = "World" });

            Assert.That(response.Result, Is.EqualTo("Hello, World!"));
            */
        }
    }
}

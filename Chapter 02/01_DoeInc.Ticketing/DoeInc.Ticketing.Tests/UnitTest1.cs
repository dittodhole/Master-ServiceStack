using DoeInc.Ticketing.Core;
using DoeInc.Ticketing.ServiceInterface;
using DoeInc.Ticketing.ServiceModel;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
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
                                                         container.RegisterAutoWired<CommentRepository>()
                                                                  .InitializedBy((container1,
                                                                                  repository) => repository.Initialize());
                                                         container.RegisterAutoWired<TicketRepository>()
                                                                  .InitializedBy((container1,
                                                                                  repository) => repository.Initialize());
                                                         container.Register<IDbConnectionFactory>(arg => new OrmLiteConnectionFactory(":memory:",
                                                                                                                                      SqliteDialect.Provider));
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
            var ticketRepository = this._appHost.Resolve<TicketRepository>();
            var storeTicket = new StoreTicket
                              {
                                  Title = "hello"
                              };
            var ticket = ticketRepository.Store(storeTicket);

            Assert.That(ticket.Id,
                        Is.GreaterThan(0));
            /*
            var service = appHost.Container.Resolve<MyServices>();

            var response = (HelloResponse)service.Any(new Hello { Name = "World" });

            Assert.That(response.Result, Is.EqualTo("Hello, World!"));
            */
        }
    }
}

using DoeInc.Ticketing.Core;
using DoeInc.Ticketing.ServiceInterface;
using DoeInc.Ticketing.ServiceModel;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace DoeInc.Ticketing.Tests
{
    [TestFixture]
    public class AppHostFunctionalTests
    {
        private const string URLBase = "http://localhost:1337/";
        private ServiceStackHost _appHost;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            this._appHost = new TestAppHost(typeof (TicketService).Assembly)
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
                            }.Init()
                             .Start(URLBase);
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            this._appHost.Dispose();
        }

        [Test]
        public void ShouldCreateTicket()
        {
            var storeTicket = new StoreTicket
                              {
                                  Title = "This is a ticket"
                              };

            using (var jsonServiceClient = new JsonServiceClient(URLBase))
            {
                var ticket = jsonServiceClient.Put(storeTicket);
                Assert.That(ticket.Id > 0);
            }
        }

        [Test]
        public void ShouldCreateTicketAndListIt()
        {
            using (var jsonServiceClient = new JsonServiceClient(URLBase))
            {
                var ticket = jsonServiceClient.Put(new StoreTicket
                                                   {
                                                       Title = "This is a ticket"
                                                   });
                Assert.That(ticket.Id > 0);
                var tickets = jsonServiceClient.Get(new GetTickets());
                Assert.That(tickets.Count > 0);
            }
        }

        [Test]
        public void ShouldGetTickets()
        {
            using (var jsonServiceClient = new JsonServiceClient(URLBase))
            {
                var tickets = jsonServiceClient.Get(new GetTickets());
                Assert.That(tickets != null);
            }
        }

        [Test]
        public void ShouldCreateTicketAndUpdate()
        {
            using (var jsonServiceClient = new JsonServiceClient(URLBase))
            {
                var ticket = jsonServiceClient.Put(new StoreTicket
                                                   {
                                                       Title = "This is a ticket"
                                                   });
                Assert.That(ticket.Id > 0);
                ticket = jsonServiceClient.Post(new StoreTicket
                                                {
                                                    Id = ticket.Id,
                                                    RowVersion = ticket.RowVersion,
                                                    Title = "A new title"
                                                });
                Assert.That(ticket.Id > 0);
                Assert.That(ticket.RowVersion > 1);
            }
        }

        [Test]
        public void ShouldCreateTicketButFailOnUpdate()
        {
            using (var jsonServiceClient = new JsonServiceClient(URLBase))
            {
                var ticketId = jsonServiceClient.Put(new StoreTicket
                                                     {
                                                         Title = "This is a ticket"
                                                     })
                                                .Id;
                Assert.That(ticketId > 0);
                Assert.Throws<WebServiceException>(() => jsonServiceClient.Post(new StoreTicket
                                                                                {
                                                                                    Id = ticketId,
                                                                                    Title = "A new title"
                                                                                }));
            }
        }
    }
}

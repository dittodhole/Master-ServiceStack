using DoeInc.Ticketing.Core;
using DoeInc.Ticketing.ServiceInterface;
using DoeInc.Ticketing.ServiceModel;
using Funq;
using ServiceStack;
using ServiceStack.Caching;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace DoeInc.Ticketing.Web
{
    public class AppHost : AppHostBase
    {
        public AppHost()
            : base("DoeInc.Ticketing",
                   typeof(TicketService).Assembly)
        {
        }

        public override void Configure(Container container)
        {
            this.RegisterRoutes();
            this.RegisterDependencies(container);
            this.RegisterPlugins();
        }

        private void RegisterRoutes()
        {
            this.Routes.Add<GetTickets>("/tickets",
                                        ApplyTo.Get)
                .Add<StoreTicket>("/tickets",
                                  ApplyTo.Post)
                .Add<StoreTicket>("/tickets/{Id}",
                                  ApplyTo.Put)
                .Add<GetTicket>("/tickets/{Id}",
                                ApplyTo.Get)
                .Add<DeleteTicket>("/tickets/{Id}",
                                   ApplyTo.Delete);

            this.Routes.Add<GetComments>("/tickets/{TicketId}/comments",
                                         ApplyTo.Get)
                .Add<StoreComment>("/tickets/{TicketId}/comments",
                                   ApplyTo.Post)
                .Add<StoreComment>("/tickets/{TicketId}/comments/{Id}",
                                   ApplyTo.Put)
                .Add<DeleteComment>("/tickets/{TicketId}/comments/{Id}",
                                    ApplyTo.Delete);
        }

        private void RegisterDependencies(Container container)
        {
            container.RegisterAutoWired<CommentRepository>()
                     .InitializedBy((arg,
                                     repository) => repository.Initialize());
            container.RegisterAutoWired<TicketRepository>()
                     .InitializedBy((arg,
                                     repository) => repository.Initialize());
            container.Register<IDbConnectionFactory>(arg => new OrmLiteConnectionFactory(":memory:",
                                                                                         SqliteDialect.Provider))
                     .ReusedWithin(ReuseScope.Hierarchy);

            container.RegisterAutoWiredAs<LegacySession, ISession>()
                     .ReusedWithin(ReuseScope.Request);
        }

        private void RegisterPlugins()
        {
            this.Plugins.Add(new SessionFeature());
        }
    }
}

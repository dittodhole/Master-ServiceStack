using DoeInc.Ticketing.Core;
using DoeInc.Ticketing.ServiceInterface;
using DoeInc.Ticketing.ServiceModel;
using Funq;
using ServiceStack;
using ServiceStack.Api.Swagger;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace DoeInc.Ticketing.Web
{
    public class AppHost : AppHostBase
    {
        /// <summary>
        ///     Default constructor.
        ///     Base constructor requires a name and assembly to locate web service classes.
        /// </summary>
        public AppHost()
            : base("DoeInc.Ticketing",
                   typeof (TicketService).Assembly)
        {
        }

        /// <summary>
        ///     Application specific configuration
        ///     This method should initialize any IoC resources utilized by your web service classes.
        /// </summary>
        /// <param name="container"></param>
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
        }

        private void RegisterPlugins()
        {
            this.Plugins.Add(new SwaggerFeature());
            this.Plugins.Add(new SessionFeature());
        }
    }
}

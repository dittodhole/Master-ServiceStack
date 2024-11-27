using DoeInc.Ticketing.Core;
using DoeInc.Ticketing.ServiceInterface;
using DoeInc.Ticketing.ServiceModel;
using Funq;
using ServiceStack;
using ServiceStack.Admin;
using ServiceStack.Auth;
using ServiceStack.Caching;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace DoeInc.Ticketing.Web
{
    public class AppHost : AppHostBase
    {
        public AppHost()
            : base("DoeInc.Ticketing",
                   typeof (TicketService).Assembly)
        {
        }

        public override void Configure(Container container)
        {
            this.RegisterPlugins();
            this.RegisterRoutes();
            this.RegisterDependencies(container);
        }

        private void RegisterPlugins()
        {
            this.Plugins.Add(new SessionFeature());
            this.Plugins.Add(new AuthFeature(() => new AuthUserSession(),
                                             new IAuthProvider[]
                                             {
                                                 new CredentialsAuthProvider(),
                                                 new BasicAuthProvider()
                                             })
                             {
                                 IncludeAssignRoleServices = false,
                                 IncludeRegistrationService = false
                             });
            this.Plugins.Add(new RequestLogsFeature
                             {
                                 EnableSessionTracking = true,
                                 EnableRequestBodyTracking = true,
                                 EnableResponseTracking = true
                             });
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
            container.Register<IDbConnectionFactory>(arg => new OrmLiteConnectionFactory("~/db.sqlite3".MapAbsolutePath(),
                                                                                         SqliteDialect.Provider))
                     .ReusedWithin(ReuseScope.Container);
            container.RegisterAs<OrmLiteCacheClient, ICacheClient>()
                     .InitializedBy((arg,
                                     cacheClient) => cacheClient.InitSchema())
                     .ReusedWithin(ReuseScope.Request);
            container.Register<IAuthRepository>(arg => new OrmLiteAuthRepository(arg.Resolve<IDbConnectionFactory>()))
                     .InitializedBy((arg,
                                     authRepository) =>
                                    {
                                        var userAuthRepository = (IUserAuthRepository) authRepository;
                                        userAuthRepository.InitSchema();

                                        if (userAuthRepository.GetUserAuthByUserName("johndoe") == null)
                                        {
                                            var userAuth = new UserAuth
                                                           {
                                                               UserName = "johndoe",
                                                               FirstName = "John",
                                                               LastName = "Doe"
                                                           };
                                            userAuthRepository.CreateUserAuth(userAuth,
                                                                              "password");
                                        }
                                        if (userAuthRepository.GetUserAuthByUserName("johnhancock") == null)
                                        {
                                            var userAuth = new UserAuth
                                                           {
                                                               UserName = "johnhancock",
                                                               FirstName = "John",
                                                               LastName = "Hancock"
                                                           };
                                            userAuthRepository.CreateUserAuth(userAuth,
                                                                              "password");
                                        }
                                    })
                     .ReusedWithin(ReuseScope.Container);
        }
    }
}

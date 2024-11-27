using Funq;
using RedisMQ.Models;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Caching;
using ServiceStack.Data;
using ServiceStack.Host;
using ServiceStack.Messaging;
using ServiceStack.Messaging.Redis;
using ServiceStack.OrmLite;
using ServiceStack.Redis;

namespace RedisMQ.ServiceB
{
    public class AppHost : AppSelfHostBase
    {
        public AppHost()
            : base("Hello Service",
                   typeof (Service).Assembly)
        {
        }

        public override void Configure(Container container)
        {
            this.RegisterRoutes();
            this.RegisterMQ(container);
            this.RegisterAuthentication(container);
        }

        private void RegisterRoutes()
        {
            this.Routes.Add<Hello>("/hello");
            this.Routes.Add<Hello>("/hello/{Name}");
        }

        private void RegisterMQ(Container container)
        {
            container.Register<IRedisClientsManager>(arg => new RedisManagerPool());

            var redisClientsManager = container.Resolve<IRedisClientsManager>();
            var redisMqServer = new RedisMqServer(redisClientsManager);
            redisMqServer.RegisterHandler<Hello>(message =>
                                                 {
                                                     var basicRequest = new BasicRequest
                                                                        {
                                                                            Verb = HttpMethods.Post
                                                                        };
                                                     basicRequest.Headers["X-" + SessionFeature.SessionId] = message.GetBody()
                                                                                                                    .SessionId;
                                                     var response = this.ServiceController.ExecuteMessage(message,
                                                                                                          basicRequest);

                                                     return response;
                                                 });
            redisMqServer.Start();

            container.Register<IMessageService>(redisMqServer);
        }

        private void RegisterAuthentication(Container container)
        {
            this.Plugins.Add(new SessionFeature());
            this.Plugins.Add(new AuthFeature(() => new AuthUserSession(),
                                             new IAuthProvider[]
                                             {
                                                 new CredentialsAuthProvider(),
                                                 new BasicAuthProvider()
                                             }));
            container.Register<IDbConnectionFactory>(arg => new OrmLiteConnectionFactory(":memory:",
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
                                    })
                     .ReusedWithin(ReuseScope.Container);
        }
    }
}

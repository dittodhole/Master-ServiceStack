using Funq;
using ServiceStack;
using ServiceStack.Admin;
using ServiceStack.Auth;
using ServiceStack.Configuration;
using ServiceStack.Data;
using ServiceStack.OrmLite;

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
            this.Config.AdminAuthSecret = "demo";

            container.Register<IDbConnectionFactory>(arg => new OrmLiteConnectionFactory(":memory:",
                                                                                         SqliteDialect.Provider))
                     .ReusedWithin(ReuseScope.Container);
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
                                            userAuth.Roles.Add(RoleNames.Admin);
                                            userAuthRepository.CreateUserAuth(userAuth,
                                                                              "password");
                                        }
                                    })
                     .ReusedWithin(ReuseScope.Container);
            this.Plugins.Add(new RequestLogsFeature
                             {
                                 EnableResponseTracking = true,
                                 EnableRequestBodyTracking = true,
                                 EnableSessionTracking = true
                             });
            this.Plugins.Add(new AuthFeature(() => new AuthUserSession(),
                                             new IAuthProvider[]
                                             {
                                                 new BasicAuthProvider(),
                                                 new CredentialsAuthProvider()
                                             })
                             {
                                 IncludeAssignRoleServices = false,
                                 IncludeRegistrationService = false
                             });

            typeof (RequestLogsService)
                .AddAttributes(new AuthenticateAttribute());
        }
    }
}

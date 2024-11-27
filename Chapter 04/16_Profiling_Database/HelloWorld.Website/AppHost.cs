using Funq;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.MiniProfiler;
using ServiceStack.MiniProfiler.Data;
using ServiceStack.OrmLite;

namespace HelloWorld.Website
{
    public class AppHost : AppHostBase
    {
        public AppHost()
            : base("Hello World",
                   typeof (HelloService).Assembly)
        {
        }

        public override void Configure(Container container)
        {
            container.Register<IDbConnectionFactory>(arg => new OrmLiteConnectionFactory("~/App_Data/db.sqlite".MapHostAbsolutePath(),
                                                                                         SqliteDialect.Provider)
                                                            {
                                                                ConnectionFilter = dbConnection => new ProfiledDbConnection(dbConnection,
                                                                                                                            Profiler.Current)
                                                            })
                     .InitializedBy((container1,
                                     factory) => factory.Open()
                                                        .CreateTableIfNotExists<Hello>());
        }
    }
}

using Funq;
using ServiceStack;
using ServiceStack.MiniProfiler;

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
            Profiler.Settings.ProfilerProvider = new CustomProfilerProvider();

            this.RawHttpHandlers.Add(request =>
                                     {
                                         CustomProfilerProvider.SetRequest(request);

                                         Profiler.Start();

                                         return null;
                                     });
            this.GlobalResponseFilters.Add((request,
                                            response,
                                            dto) =>
                                           {
                                               Profiler.Stop();
                                           });
        }
    }
}

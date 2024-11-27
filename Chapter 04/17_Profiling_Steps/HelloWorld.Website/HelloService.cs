using ServiceStack;
using ServiceStack.MiniProfiler;

namespace HelloWorld.Website
{
    public class HelloService : IService,
                                IAny<Hello>
    {
        public object Any(Hello request)
        {
            var profiler = Profiler.Current;

            using (profiler.Step("Custom Step"))
            {
                var name = request.Name;
                var helloResponse = new HelloResponse
                                    {
                                        Result = "Hello {0}".Fmt(name)
                                    };

                return helloResponse;
            }
        }
    }
}

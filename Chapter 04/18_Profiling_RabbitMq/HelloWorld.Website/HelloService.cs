using ServiceStack;
using ServiceStack.MiniProfiler;

namespace HelloWorld.Website
{
    public class HelloService : IService,
                                IAny<Hello>
    {
        public object Any(Hello request)
        {
            using (Profiler.Current.Step("Custom Step"))
            {
                var name = request.Name;
                using (Profiler.StepStatic("Inner custom step"))
                {
                    var helloResponse = Profiler.Current.Inline(() => new HelloResponse
                                                                {
                                                                    Result = "Hello {0}".Fmt(name)
                                                                },
                                                                "Inline Step");
                    return helloResponse;
                }
            }
        }
    }
}

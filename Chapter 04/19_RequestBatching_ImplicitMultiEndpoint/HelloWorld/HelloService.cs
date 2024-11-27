using HelloWorld.Models;
using ServiceStack;

namespace HelloWorld
{
    public class HelloService : IService,
                                IAny<Hello>
    {
        public object Any(Hello request)
        {
            var helloResponse = HelloService.DoWork(request);

            return helloResponse;
        }

        public object Any(Hello[] requests)
        {
            return requests.Map(HelloService.DoWork);
        }

        private static HelloResponse DoWork(Hello hello)
        {
            return new HelloResponse
                   {
                       Result = "Hello {0}".Fmt(hello.Name)
                   };
        }
    }
}

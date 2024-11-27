using HelloWorld.Models;
using ServiceStack;

namespace HelloWorld
{
    public class HelloService : Service,
                                IAny<Hello>,
                                IAny<Hello[]>
    {
        public object Any(Hello[] request)
        {
            return request.Map(HelloService.DoWork);
        }

        public object Any(Hello request)
        {
            var helloResponse = HelloService.DoWork(request);

            return helloResponse;
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

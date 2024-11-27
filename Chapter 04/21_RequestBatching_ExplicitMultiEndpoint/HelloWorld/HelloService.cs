using HelloWorld.Models;
using ServiceStack;

namespace HelloWorld
{
    public class HelloService : IService,
                                IAny<Hello>,
                                IAny<Hellos>
    {
        public object Any(Hello request)
        {
            var helloResponse = HelloService.DoWork(request);

            return helloResponse;
        }

        public object Any(Hellos request)
        {
            return request.Map(HelloService.DoWork);
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

using ServiceStack;

namespace HelloWorld
{
    public class HelloService : IService,
                                IAny<Hello>
    {
        public object Any(Hello request)
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

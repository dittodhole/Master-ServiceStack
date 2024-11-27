using ServiceStack;

namespace HelloWorld.Website
{
    public class HelloService : IService,
                                IAny<Hello>
    {
        public object Any(Hello request)
        {
            var name = request.Name;
            var result = "Hello {0}".Fmt(name);

            return result;
        }
    }
}

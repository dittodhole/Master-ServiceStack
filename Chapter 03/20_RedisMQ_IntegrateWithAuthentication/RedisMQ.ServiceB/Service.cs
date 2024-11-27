using RedisMQ.Models;
using ServiceStack;
using ServiceStack.Text;

namespace RedisMQ.ServiceB
{
    public class Service : ServiceStack.Service,
                           IAny<Hello>
    {
        [Authenticate]
        public object Any(Hello request)
        {
            "incoming request for hello".Print();

            var name = request.Name;
            var helloResponse = new HelloResponse
                                {
                                    Result = "Hello {0}".Fmt(name)
                                };

            return helloResponse;
        }
    }
}

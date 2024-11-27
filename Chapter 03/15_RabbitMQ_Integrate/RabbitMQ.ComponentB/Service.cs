using RabbitMQ.Models;
using ServiceStack;

namespace RabbitMQ.ComponentB
{
    public class Service : ServiceStack.Service,
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

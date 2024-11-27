using RedisMQ.Models;
using ServiceStack;

namespace RedisMQ.ServiceB
{
    public class Service : ServiceStack.Service,
                           IAny<Hello>,
                           IAnyVoid<GenerateHello>
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

        public void Any(GenerateHello request)
        {
            var name = request.Name;
            var hello = new Hello
                        {
                            Name = name
                        };

            this.PublishMessage(hello);
        }
    }
}

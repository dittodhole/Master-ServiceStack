using ServiceStack;
using ServiceStack.OrmLite;

namespace HelloWorld.Website
{
    public class HelloService : Service,
                                IAny<Hello>
    {
        public object Any(Hello request)
        {
            for (var i = 0;
                 i < 10;
                 i++)
            {
                var hello = this.Db.Select<Hello>();
            }

            var name = request.Name;
            var helloResponse = new HelloResponse
                                {
                                    Result = "Hello {0}".Fmt(name)
                                };

            return helloResponse;
        }
    }
}

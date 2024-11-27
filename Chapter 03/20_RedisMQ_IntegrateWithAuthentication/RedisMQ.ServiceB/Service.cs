using RedisMQ.Models;
using ServiceStack;
using ServiceStack.Text;

namespace RedisMQ.ServiceB
{
    public class Service : ServiceStack.Service,
                           IAnyVoid<Hello>
    {
        [Authenticate]
        public void Any(Hello request)
        {
            var session = this.GetSession();
            var name = "{0} {1}".Fmt(session.FirstName,
                                     session.LastName);
            var result = "Hello {0}".Fmt(name);

            result.Print();
        }
    }
}

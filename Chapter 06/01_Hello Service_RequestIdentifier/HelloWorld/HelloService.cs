using ServiceStack;

namespace HelloWorld
{
    public class HelloService : Service,
                                IAny<Hello>
    {
        public object Any(Hello request)
        {
            var result = "Hello {0}".Fmt(request.Name);
            if (request.Volume == Volume.Loud)
            {
                result = result.ToUpper();
            }
            else if (request.Volume == Volume.Gentle)
            {
                result = result.ToLower();
            }

            var helloResponse = new HelloResponse
                                {
                                    Result = result
                                };

            return helloResponse;
        }
    }
}

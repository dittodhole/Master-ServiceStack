using ServiceStack;

namespace HelloWorld
{
    public class HelloService : IService,
                                IAny<Hello>
    {
        public object Any(Hello request)
        {
            string name;
            if (request.Version == 0)
            {
                name = request.Name;
            }
            else if (request.Version == 1)
            {
                name = string.Concat(request.Forename,
                                     request.Surname);
            }
            else
            {
                throw HttpError.Conflict("The version provided is not supported.");
            }

            var result = "Hello {0}".Fmt(name);
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

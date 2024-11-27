using System.Net;
using ServiceStack;

namespace HelloWorld
{
    [Api("Endpoint to greet a person.")]
    [ApiResponse(HttpStatusCode.InternalServerError, "There was an exception during greeting.")]
    [Route("/hello", Summary = "Greets a person.", Notes = "Longer description.")]
    public class Hello : IReturn<HelloResponse>
    {
        [ApiMember(Description = "Defines the person to greet.", AllowMultiple = true)]
        public string Name { get; set; }

        [ApiMember(IsRequired = true, Description = "Defines the volume of the greeting.")]
        [ApiAllowableValues("Volume", typeof (Volume))]
        public Volume Volume { get; set; }
    }
}

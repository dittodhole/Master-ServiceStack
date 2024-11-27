using ServiceStack;
using ServiceStack.DataAnnotations;

namespace HelloWorld
{
    [Api("Endpoint to greet a person.")]
    [Route("/hello/{Name}", Summary = "Greets a person.")]
    //[Exclude(Feature.Metadata)]
    public class Hello : IReturn<HelloResponse>
    {
        [ApiMember(IsRequired = true, Description = "Defines the person to greet.", AllowMultiple = true)]
        public string Name { get; set; }

        [ApiMember(IsRequired = true, Description = "Defines the volume of the greeting.")]
        [ApiAllowableValues("Volume", typeof (Volume))]
        public Volume Volume { get; set; }
    }
}

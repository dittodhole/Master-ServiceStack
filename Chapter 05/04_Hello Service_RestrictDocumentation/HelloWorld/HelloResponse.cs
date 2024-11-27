using ServiceStack;

namespace HelloWorld
{
    public class HelloResponse
    {
        [ApiMember(IsRequired = true, Description = "The greeting.")]
        public string Result { get; set; }
    }
}

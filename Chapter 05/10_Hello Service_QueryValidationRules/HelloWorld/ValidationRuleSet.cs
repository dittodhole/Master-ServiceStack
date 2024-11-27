using ServiceStack;

namespace HelloWorld
{
    [Route("/validation/{TypeName}")]
    public class ValidationRuleSet : IReturn<ValidationRuleSetResponse>
    {
        public string TypeName { get; set; }
    }
}

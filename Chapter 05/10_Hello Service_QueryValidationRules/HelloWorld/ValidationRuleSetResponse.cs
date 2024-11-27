using System.Collections.Generic;
using ServiceStack;

namespace HelloWorld
{
    public class ValidationRuleSetResponse : IHasResponseStatus
    {
        public List<ValidationRule> ValidationRules { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }
}

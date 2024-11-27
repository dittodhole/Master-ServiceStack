using ServiceStack.FluentValidation;

namespace HelloWorld.Validators
{
    public class HelloValidator : AbstractValidator<Hello>
    {
        public HelloValidator()
        {
            this.RuleFor(arg => arg.Name)
                .NotEmpty();
            this.RuleFor(arg => arg.Volume)
                .Equal(Volume.Gentle);
        }
    }
}

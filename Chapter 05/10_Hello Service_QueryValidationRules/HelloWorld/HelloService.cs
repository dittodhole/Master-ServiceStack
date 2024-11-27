using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack;
using ServiceStack.FluentValidation;
using ServiceStack.FluentValidation.Internal;
using ServiceStack.FluentValidation.Validators;

namespace HelloWorld
{
    public class HelloService : Service,
                                IAny<Hello>,
                                IAny<ValidationRuleSet>
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

        public object Any(ValidationRuleSet request)
        {
            var typeName = request.TypeName;
            var assembly = typeof (HelloService).Assembly;
            var type = assembly.GetExportedTypes()
                               .FirstOrDefault(arg => string.Equals(arg.Name,
                                                                    typeName,
                                                                    StringComparison.OrdinalIgnoreCase));
            if (type == null)
            {
                throw HttpError.NotFound("Type {0} cannot be found".Fmt(typeName));
            }

            var validatorType = typeof (IValidator<>).MakeGenericType(type);

            var methodInfo = typeof (Service).GetMethod("TryResolve");
            var validator = (IValidator) methodInfo.MakeGenericMethod(validatorType)
                                                   .Invoke(this,
                                                           null);
            if (validator == null)
            {
                throw HttpError.NotFound("Validator for type {0} cannot be found".Fmt(typeName));
            }

            return this.GetValdationRuleSetResponse(type,
                                                    validator);
        }

        private object GetValdationRuleSetResponse(Type type,
                                                   IValidator validator)
        {
            var instance = type.CreateInstance();
            var validationContext = new ValidationContext(instance);

            var validatorDescriptor = validator.CreateDescriptor();
            var rules = validatorDescriptor.GetMembersWithValidators()
                                           .Select(arg =>
                                                   {
                                                       var propertyName = arg.Key;

                                                       return this.GetValidationRulesOfProperty(propertyName,
                                                                                                validatorDescriptor,
                                                                                                validationContext);
                                                   })
                                           .ToList();
            var response = new ValidationRuleSetResponse
                           {
                               Rules = rules
                           };

            return response;
        }

        private ValidationRulesOfProperty GetValidationRulesOfProperty(string propertyName,
                                                                       IValidatorDescriptor validatorDescriptor,
                                                                       ValidationContext validationContext)
        {
            // TODO IDelegateValidator is not supported in this scenario

            var validationRules = validatorDescriptor.GetRulesForMember(propertyName)
                                                     .OfType<PropertyRule>()
                                                     .SelectMany(rule => this.GetValidationRules(validationContext,
                                                                                                 rule,
                                                                                                 propertyName))
                                                     .ToList();
            var validationRule = new ValidationRulesOfProperty
                                 {
                                     PropertyName = propertyName,
                                     ValidationRules = validationRules
                                 };
            return validationRule;
        }

        private IEnumerable<string> GetValidationRules(ValidationContext validationContext,
                                                       PropertyRule rule,
                                                       string propertyName)
        {
            var propertyContext = new PropertyValidatorContext(validationContext,
                                                               rule,
                                                               propertyName);
            propertyContext.MessageFormatter.AppendPropertyName(propertyName);

            var validationRules = rule.Validators.Select(validator =>
                                                         {
                                                             var comparisonValidator = validator as IComparisonValidator;
                                                             if (comparisonValidator != null)
                                                             {
                                                                 propertyContext.MessageFormatter.AppendArgument("PropertyValue",
                                                                                                                 comparisonValidator.ValueToCompare);
                                                             }

                                                             // TODO add more arguments

                                                             var messageTemplate = validator.ErrorMessageSource.GetString();
                                                             var message = propertyContext.MessageFormatter.BuildMessage(messageTemplate);

                                                             return message;
                                                         });

            return validationRules;
        }
    }
}

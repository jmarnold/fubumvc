using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using FubuCore.Reflection;

namespace FubuValidation.Strategies
{
    public class EmailFieldStrategy : IFieldValidationStrategy
    {
        private static readonly Regex EmailRegex = new Regex(@"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        
        public static readonly string FIELD = "field";
        
        public IEnumerable<KeyValuePair<string, string>> GetMessageSubstitutions(Accessor accessor)
        {
            return new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>(FIELD, accessor.Name)
                        };
        }

        public ValidationStrategyResult Validate(object target, object rawValue, Type declaringType, Notification notification)
        {
            if(rawValue == null || !EmailRegex.IsMatch(rawValue.ToString()))
            {
                return ValidationStrategyResult.Invalid(new NotificationMessage(ValidationKeys.EMAIL));
            }

            return ValidationStrategyResult.Valid();
        }
    }
}
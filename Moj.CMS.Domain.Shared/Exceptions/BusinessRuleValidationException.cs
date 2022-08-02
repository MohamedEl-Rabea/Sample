using Moj.CMS.Domain.Shared.Entities;

namespace Moj.CMS.Domain.Shared.Exceptions
{
    public class BusinessRuleValidationException : AppExceptionBase
    {
        public IBusinessRuleBase BrokenRule { get; }

        public string Details { get; }

        public BusinessRuleValidationException(IBusinessRuleBase brokenRule)
              : base(brokenRule.Message)
        {
            BrokenRule = brokenRule;
            Details = brokenRule.Message;
        }

        public override string ToString()
        {
            return $"{BrokenRule.GetType().FullName}: {BrokenRule.Message}";
        }
    }
}

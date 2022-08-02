using Moj.CMS.Domain.Shared.Audit;
using Moj.CMS.Domain.Shared.Guard;

namespace Moj.CMS.Domain.Aggregates.Court.Entities
{
    public class Division : AuditedEntity
    {
        private Division()
        {

        }

        public string Name { get; private set; }
        public string Code { get; private set; }
        public bool IsActive { get; private set; }

        public static Division Create(string name, string code, bool isActive)
        {
            Guard.AssertArgumentNotNullOrEmptyOrWhitespace(name, nameof(name));
            Guard.AssertArgumentNotNullOrEmptyOrWhitespace(code, nameof(code));

            return new Division
            {
                Name = name,
                Code = code,
                IsActive = isActive
            };
        }
    }
}

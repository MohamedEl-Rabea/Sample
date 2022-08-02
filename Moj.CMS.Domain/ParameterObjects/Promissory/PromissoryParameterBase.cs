using Moj.CMS.Domain.DomainServices;
using Moj.CMS.Domain.Shared.Enums;

namespace Moj.CMS.Domain.ParameterObjects.Promissory
{
    public class PromissoryParameterBase
    {
        public string Number { get; set; }
        public PromissoryTypeEnum PromissoryTypeId { get; set; }
        public IEnforcePromissoryNumberIsUnique EnforcePromissoryNumberIsUnique { get; set; }
    }
}

using Moj.CMS.Domain.Aggregates.Iban.ValueObjects;
using Moj.CMS.Domain.DomainServices;
using Moj.CMS.Domain.Shared.Enums;

namespace Moj.CMS.Domain.ParameterObjects.Iban
{
    public class CreateIbanParam
    {
        public string IbanNumber { get; set; }
        public string Bank { get; set; }
        public string Branch { get; set; }
        public IbanReferenceDetails IbanReferenceDetails { get; private set; }
        public int VIbanQuantity { get; private set; }
        public int VIbanRemaining { get; private set; }
        public IEnforceIbanNumberIsUnique EnforceIbanNumberIsUnique { get; set; }
    }
}
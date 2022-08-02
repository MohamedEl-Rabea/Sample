using Moj.CMS.Domain.Aggregates.Iban.BusinessRules;
using Moj.CMS.Domain.Aggregates.Iban.ValueObjects;
using Moj.CMS.Domain.ParameterObjects.Iban;
using Moj.CMS.Domain.Shared.Audit;
using Moj.CMS.Domain.Shared.Entities;
using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Domain.Shared.Guard;
using System.Threading.Tasks;

namespace Moj.CMS.Domain.Aggregates.Iban
{
    public class Iban : FullAuditedAggregateRoot, IAggregateRoot
    {
        #region Constructor

        public Iban()
        {
        }

        #endregion Constructor

        #region properties

        public string Number { get; private set; }
        public string Bank { get; private set; }
        public string Branch { get; private set; }
        public int VIbanQuantity { get; private set; }
        public int VIbanRemaining { get; private set; }
        public bool IsActive { get; private set; }
        public IbanReferenceDetails IbanReferenceDetails { get; private set; }

        #endregion properties

        #region Methods

        public static async Task<Iban> Create(CreateIbanParam param)
        {
            Guard.AssertArgumentNotNullOrEmptyOrWhitespace(param.IbanNumber, nameof(param.IbanNumber));
            Guard.AssertArgumentNotNullOrEmptyOrWhitespace(param.Bank, nameof(param.Bank));
            Guard.AssertArgumentNotNullOrEmptyOrWhitespace(param.Branch, nameof(param.Branch));
            Guard.AssertArgumentNotLessThanOrEqualToZero(param.VIbanRemaining, nameof(param.VIbanRemaining));
            Guard.AssertArgumentNotLessThanOrEqualToZero(param.VIbanQuantity, nameof(param.VIbanQuantity));
            var Iban = new Iban();
            await Iban.SetIbanBasicInfoAsync(param);
            return Iban;
        }

        #endregion Methods

        #region HelperMethods

        private async Task SetIbanBasicInfoAsync(CreateIbanParam param)
        {
            await CheckRuleAsync(new NoDuplicateIbanAllowedBusniessRule(param.IbanNumber, param.EnforceIbanNumberIsUnique));

            Number = param.IbanNumber;
            Bank = param.Bank;
            Branch = param.Branch;
            VIbanQuantity = param.VIbanQuantity;
            VIbanRemaining = param.VIbanRemaining;
            IbanReferenceDetails = param.IbanReferenceDetails;
            IsActive = true;
        }

        #endregion HelperMethods
    }
}
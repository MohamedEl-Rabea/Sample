using Moj.CMS.Domain.ParameterObjects.Case;
using Moj.CMS.Domain.Shared.Audit;
using Moj.CMS.Domain.Shared.Guard;
using System;

namespace Moj.CMS.Domain.Aggregates.Case.Entities
{
    public class CaseSadad : AuditedEntity
    {
        private CaseSadad()
        {

        }

        public string SadadNumber { get; private set; }
        public int PartyId { get; private set; }
        public decimal? CAP { get; private set; }
        public decimal RequiredAmount { get; private set; }
        public DateTime IssueDate { get; private set; }
        public bool IsActive { get; private set; }

        public static CaseSadad Create(CreateSadadParam param)
        {
             var sadad = new CaseSadad();
            sadad.SetSadadBasicInfo(param);
            sadad.IssueDate = param.IssueDate;
            return sadad;
        }
        public void Update(UpdateSadadParam param)
        {
            SetSadadBasicInfo(param);
        }
        public void Deactivate()
        {
            //TODO: once B2B integration is done, should make sure to closing the actual account 
            IsActive = false;
        }
        private void SetSadadBasicInfo(SadadBasicInfoParam param)
        {
            Validate(param);
            SadadNumber = param.SadadNumber;
            PartyId = param.PartyId;
            CAP = param.CAP;
            RequiredAmount = param.RequiredAmount;
            IsActive = true;
        }
        private void Validate(SadadBasicInfoParam param)
        {
            Guard.AssertArgumentNotNullOrEmptyOrWhitespace(param.SadadNumber, nameof(param.SadadNumber));
            Guard.AssertNullableArgumentNotLessThanOrEqualToZero(param.RequiredAmount, nameof(param.RequiredAmount));
            Guard.AssertNullableArgumentNotLessThanOrEqualToZero(param.CAP, nameof(param.CAP));
        }
    }
}

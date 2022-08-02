using Moj.CMS.Domain.Shared.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Moj.CMS.Domain.Shared.LookupModels
{
    [Table("IbanPurposeLookup")]
    public class IbanPurpose : LookupBase
    {

    }

    [Table("VIbanReferenceTypeLookup")]
    public class VIbanReferenceType : LookupBase
    {

    }

    [Table("IntegrationRequestStatusLookup")]
    public class IntegrationRequestStatus : LookupBase
    {

    }

    [Table("CaseStatusesLookup")]
    public class CaseStatus : LookupBase
    {

    }

    [Table("PromissoryTypesLookup")]
    public class PromissoryType : LookupBase
    {
        [Display(Order = 3)]
        public string Description { get; set; }
        public override IList<string> GetPropertiesNames()
        {
            return new List<string> { nameof(Id), nameof(Name), nameof(Description) }.ToList();
        }
        public override IList<string> GetExportablePropertiesNames()
        {
            return new List<string> { nameof(Name), nameof(Description) }.ToList();
        }
    }

    [Table("CaseTypesLookup")]
    public class CaseType : LookupBase
    {
        public override IList<string> GetPropertiesNames()
        {
            return new List<string> { nameof(Id), nameof(Name) };
        }
    }

    [Table("CaseOperationsLookup")]
    public class CaseOperation : LookupBase
    {
        public override IList<string> GetPropertiesNames()
        {
            return new List<string> { nameof(Id), nameof(Name) };
        }
    }

    [Table("PartyFinancialTypesLookup")]
    public class PartyFinancialType : LookupBase
    {
    }

    [Table("PartyLocationsLookup")]
    public class PartyLocation : LookupBase
    {
    }

    [Table("PartyStatusesLookup")]
    public class PartyStatus : LookupBase
    {
    }

    [Table("PartyIdentityTypesLookup")]
    public class PartyIdentityType : LookupBase
    {
    }

    [Table("PartyTypesLookup")]
    public class PartyType : LookupBase
    {
    }

    [Table("JudgesLookup")]
    public class Judge : LookupBase
    {
        [MaxLength(15)]
        [Display(Order = 1)]
        public string Code { get; set; }
        public bool IsActive { get; set; }

        protected override bool? GetStatus()
        {
            return IsActive;
        }

        public override IList<string> GetPropertiesNames()
        {
            return new List<string> { nameof(Id), nameof(Code), nameof(Name), nameof(IsActive) };
        }
        public override IList<string> GetExportablePropertiesNames()
        {
            return base.GetExportablePropertiesNames().Concat(new List<string> { nameof(Code), nameof(StatusText) }).ToList();
        }
        public override IEnumerable<LookupFilterInfo> GetFilterableProperties()
        {
            return new List<LookupFilterInfo>
            {
                new LookupFilterInfo { Order=1, Name = nameof(Code), PropertyType = PropertyType.String },
                new LookupFilterInfo { Order=2, Name = nameof(Name), PropertyType = PropertyType.String },
                new LookupFilterInfo { Order=3, Name = nameof(IsActive), PropertyType = PropertyType.Status },
            };
        }
    }

    [Table("NationalityLookup")]
    public class Nationality : LookupBase
    {
        [StringLength(5, MinimumLength = 5)]
        [Display(Order = 1)]
        public string? Code { get; set; }
        [StringLength(2, MinimumLength = 2)]
        [Display(Order = 3)]
        public string? A2 { get; set; }
        [StringLength(3, MinimumLength = 3)]
        [Display(Order = 4)]
        public string? A3 { get; set; }

        public override IList<string> GetPropertiesNames()
        {
            return base.GetPropertiesNames().Concat(new List<string> { nameof(Code), nameof(A2), nameof(A3) }).ToList();
        }
        public override IList<string> GetExportablePropertiesNames()
        {
            return base.GetExportablePropertiesNames().Concat(new List<string> { nameof(Code), nameof(A2), nameof(A3) }).ToList();
        }
    }

    [Table("RequestTerminationReasonsLookup")]
    public class RequestTerminationReasons : LookupBase
    {
    }

    [Table("PartyRolesLookup")]
    public class PartyRole : LookupBase
    {
    }

    [Table("PartyClassificationLookup")]
    public class PartyClassification : LookupBase
    {
    }

    [Table("AreasLookup")]
    public class Area : LookupBase
    {
        [Display(Order = 1)]
        public string Code { get; set; }
        public bool IsActive { get; set; }

        protected override bool? GetStatus()
        {
            return IsActive;
        }
        public override IList<string> GetPropertiesNames()
        {
            return new List<string> { nameof(Code), nameof(Name), nameof(IsActive) }.ToList();
        }
        public override IList<string> GetExportablePropertiesNames()
        {
            return new List<string> { nameof(Code), nameof(Name), nameof(StatusText) }.ToList();
        }
        public override IEnumerable<LookupFilterInfo> GetFilterableProperties()
        {
            return base.GetFilterableProperties().Concat(new List<LookupFilterInfo>
            {
                new LookupFilterInfo { Order=2, Name = nameof(Code), PropertyType = PropertyType.String },
                new LookupFilterInfo { Order=3, Name = nameof(IsActive), PropertyType = PropertyType.Status },
            });
        }
    }

    [Table("DebtTypesLookup")]
    public class DebtType : LookupBase
    {
    }

    [Table("ClaimFinancialStatusesLookup")]
    public class ClaimFinancialStatus : LookupBase
    {
    }

    [Table("ClaimStatusesLookup")]
    public class ClaimStatus : LookupBase
    {
    }

    [Table("FinancialEffectsLookup")]
    public class FinancialEffectType : LookupBase
    {
        public bool IsIncrementOnClaim { get; set; }
    }

    [Table("ClaimTerminationReasonLookup")]
    public class ClaimTerminationReason : LookupBase
    {
    }
}

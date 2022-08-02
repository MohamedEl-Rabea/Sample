using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Domain.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Moj.CMS.Domain.Shared.LookupModels
{
    public class LookupTypesEnum : BaseEnumeration
    {
        public Type LookupType { get; set; }
        public string QueryMethod { get; set; }
        public bool IsSystem { get; set; }

        public static LookupTypesEnum CaseStatuses = new LookupTypesEnum(1, "Case Statuses", "GetCaseStatusListAsync", isSystem: true, typeof(CaseStatus));
        public static LookupTypesEnum CaseTypes = new LookupTypesEnum(2, "Case Types", "GetCaseTypeListAsync", isSystem: true, typeof(CaseType));
        public static LookupTypesEnum Judges = new LookupTypesEnum(3, "Judges", "GetJudgeListAsync", isSystem: false, typeof(Judge));
        public static LookupTypesEnum PromissoryTypes = new LookupTypesEnum(4, "Promissory Types", "GetPromissoryTypeListAsync", isSystem: true, typeof(PromissoryType));
        public static LookupTypesEnum Areas = new LookupTypesEnum(5, "Areas", "GetAreaListAsync", isSystem: false, typeof(Area));
        public static LookupTypesEnum PartyTypes = new LookupTypesEnum(6, "PartyTypes", "GetPartyTypeListAsync", isSystem: true, typeof(PartyType));
        public static LookupTypesEnum PartyRoles = new LookupTypesEnum(7, "PartyRoles", "GetPartyRoleListAsync", isSystem: true, typeof(PartyRole));
        public static LookupTypesEnum PartyIdentityTypes = new LookupTypesEnum(8, "Party Identity Types", "GetPartyIdentityTypeListAsync", isSystem: true, typeof(PartyIdentityType));
        public static LookupTypesEnum DebtTypes = new LookupTypesEnum(9, "Debt Types", "GetDebtTypeListAsync", isSystem: true, typeof(DebtType));
        public static LookupTypesEnum PartyLocations = new LookupTypesEnum(10, "Party Locations", "GetPartyLocationListAsync", isSystem: true, typeof(PartyLocation));
        public static LookupTypesEnum ClaimStatuses = new LookupTypesEnum(11, "Financial Claim Statuses", "GetFinancialClaimStatuseListAsync", isSystem: true, typeof(ClaimStatus));
        public static LookupTypesEnum ClaimFinancialStatuses = new LookupTypesEnum(12, "Financial Claim Statuses", "GetClaimFinancialStatusListAsync", isSystem: true, typeof(ClaimFinancialStatus));
        
        public LookupTypesEnum(int id, string name, string queryMethod, bool isSystem, Type lookupType) : base(id, name)
        {
            QueryMethod = queryMethod;
            IsSystem = isSystem;
            LookupType = lookupType;
        }

        public IEnumerable<string> GetProperties()
        {
            var instance = (LookupBase)Activator.CreateInstance(LookupType);
            return instance.GetPropertiesNames();
        }

        public IEnumerable<string> GetExportableProperties()
        {
            var instance = (LookupBase)Activator.CreateInstance(LookupType);
            return instance.GetExportablePropertiesNames();
        }

        public IEnumerable<LookupFilterInfo> GetFilterableProperties()
        {
            var instance = (LookupBase)Activator.CreateInstance(LookupType);
            return instance.GetFilterableProperties().OrderBy(f => f.Order);
        }

        public static IEnumerable<LookupTypesEnum> List()
        {
            return new List<LookupTypesEnum>
            {
                CaseStatuses,
                CaseTypes,
                Judges,
                PromissoryTypes,
                Areas,
                PartyTypes,
                PartyRoles,
                PartyIdentityTypes,
                DebtTypes,
                PartyLocations,
                ClaimFinancialStatuses,
                ClaimStatuses
            };
        }

        public static LookupTypesEnum Find(int id)
        {
            var lookupType = List().FirstOrDefault(l => l.Id == id);
            if (lookupType == null)
                throw new Exception($"Lookup type with id {id} not found ");

            return lookupType;
        }
    }
}

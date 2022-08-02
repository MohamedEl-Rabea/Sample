using Moj.CMS.Domain.Shared.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Moj.CMS.Domain.Lookups
{
    public class FinancialEffectTypeLookUp: BaseEnumeration
    {
        public bool IsIncremental { get; set; }

        public static FinancialEffectTypeLookUp WaiverRecord = new FinancialEffectTypeLookUp((int)FinancialEffectTypeEnum.WaiverRecord, nameof(FinancialEffectTypeEnum.WaiverRecord), isIncremental: false);
        public static FinancialEffectTypeLookUp ServiceProvider = new FinancialEffectTypeLookUp((int)FinancialEffectTypeEnum.ServiceProvider, nameof(FinancialEffectTypeEnum.ServiceProvider), isIncremental: true);
        public static FinancialEffectTypeLookUp NewspaperAdvertisement = new FinancialEffectTypeLookUp((int)FinancialEffectTypeEnum.NewspaperAdvertisement, nameof(FinancialEffectTypeEnum.NewspaperAdvertisement), isIncremental: true);
        public FinancialEffectTypeLookUp(int id, string name, bool isIncremental) : base(id, name)
        {
            IsIncremental = isIncremental;
        }

        public static FinancialEffectTypeLookUp Find(FinancialEffectTypeEnum FinancialEffectType)
        {
            return List().FirstOrDefault(r => r.Id == (int)FinancialEffectType);
        }

        public static IEnumerable<FinancialEffectTypeLookUp> List()
        {
            yield return WaiverRecord;
            yield return ServiceProvider;
            yield return NewspaperAdvertisement;
        }
    }
}

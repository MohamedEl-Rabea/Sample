using Moj.CMS.Domain.Shared.Audit;
using Moj.CMS.Domain.Shared.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moj.CMS.Domain.Shared.LookupModels
{
    public class LookupBase : CreationAuditedEntity
    {
        [MaxLength(50)]
        [Display(Order = 2)]
        public string Name { get; set; }

        private string _statusText;
        [NotMapped]
        [Display(Order = 5)]
        public string StatusText
        {
            set
            {
                _statusText = value;
            }
            get
            {
                if (string.IsNullOrEmpty(_statusText))
                    return GetStatus() == null ? "N/A" : GetStatus().Value ? "Active" : "InActive";

                return _statusText;
            }
        }

        protected virtual bool? GetStatus()
        {
            return null;
        }

        public virtual IList<string> GetPropertiesNames()
        {
            return new List<string>
            {
                nameof(Id),
                nameof(Name)
            };
        }

        public virtual IList<string> GetExportablePropertiesNames()
        {
            return new List<string>
            {
                nameof(Name)
            };
        }

        public virtual IEnumerable<LookupFilterInfo> GetFilterableProperties()
        {
            return new List<LookupFilterInfo>
            {
                new LookupFilterInfo { Order = 1, Name = nameof(Name), PropertyType = PropertyType.String }
            };
        }
    }
}

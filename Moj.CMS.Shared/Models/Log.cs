using Moj.CMS.Domain.Shared.Audit;
using Moj.CMS.Shared.CustomAttributes;
using Moj.CMS.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moj.CMS.Shared.Models
{
    [Table("Logs", Schema = "EventLogging")]
    [IgnoreAuditing]
    public class Log : CreationAuditedEntity
    {
        public RequestType RequestType { get; set; }
        public string Message { get; set; }

        public string MessageShortcut => Message.Substring(0, Message.Length > 30 ? 30 : Message.Length);

        [MaxLength(50)]
        public string Status { get; set; }

        public string Exception { get; set; }

        public long ExecutionTime { get; set; }
        [MaxLength(50)]
        public string RequestId { get; set; }

        [MaxLength(256)]
        public string RequestName { get; set; }

        [MaxLength(256)]
        public string UserName { get; set; }

        public string InputDetails { get; set; }

    }
}

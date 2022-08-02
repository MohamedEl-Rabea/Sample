using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moj.CMS.Domain.Shared.Values;
using Moj.CMS.Shared.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Moj.CMS.Shared.Models.Audit
{
    public class AuditEntry
    {
        public AuditEntry(EntityEntry entry)
        {
            Entry = entry;
        }

        public EntityEntry Entry { get; }
        public Guid RequestId { get; set; }
        public string RequestName { get; set; }
        public string UserId { get; set; }
        public string TableName { get; set; }
        public string KeyValue { get; set; }
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
        public List<PropertyEntry> TemporaryProperties { get; } = new List<PropertyEntry>();
        public AuditType AuditType { get; set; }
        public List<string> ChangedColumns { get; } = new List<string>();
        public bool HasTemporaryProperties => TemporaryProperties.Any();

        public Audit ToAudit()
        {
            var audit = new Audit();
            audit.UserId = UserId;
            audit.RequestId = RequestId;
            audit.RequestName = RequestName;
            audit.Type = AuditType.ToString();
            audit.EntityName = TableName;
            audit.DateTime = CLock.Now;
            audit.EntityId = KeyValue;
            audit.CreatorUserId = UserId;
            audit.CreationTime = CLock.Now;
            audit.OldValues = OldValues.Count == 0 ? null : JsonConvert.SerializeObject(OldValues);
            audit.NewValues = NewValues.Count == 0 ? null : JsonConvert.SerializeObject(NewValues);
            audit.AffectedColumns = ChangedColumns.Count == 0 ? null : JsonConvert.SerializeObject(ChangedColumns);
            return audit;
        }
    }
}
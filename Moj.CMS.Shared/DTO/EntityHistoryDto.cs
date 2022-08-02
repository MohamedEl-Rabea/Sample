using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Moj.CMS.Shared.Models
{
    public class EntityHistoryDto
    {
        public int Id { get; set; }
        public string RequestId { get; set; }
        public string RequestName { get; set; }
        public string OperationType { get; set; }
        public string EntityId { get; set; }
        public string EntityName { get; set; }
        public DateTime CreationDate { get; set; }
        public string UserName { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }

        public List<EntityChangesDto> GetEntityChanges()
        {
            List<EntityChangesDto> tableData = new List<EntityChangesDto>();

            var oldValuesList = OldValues == null ? default : JsonConvert.DeserializeObject<Dictionary<string, object>>(OldValues);
            var newValuesList = NewValues == null ? default : JsonConvert.DeserializeObject<Dictionary<string, object>>(NewValues);

            var affectedProps = oldValuesList ?? newValuesList;

            if (affectedProps != null)
                tableData = affectedProps.Select(prop => new EntityChangesDto
                {
                    PropertyName = prop.Key,
                    OldValue = oldValuesList?[prop.Key]?.ToString() ?? "---",
                    NewValue = newValuesList?[prop.Key]?.ToString() ?? "---"
                }).ToList();

            return tableData;
        }
    }

    public class EntityChangesDto
    {
        public string PropertyName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}
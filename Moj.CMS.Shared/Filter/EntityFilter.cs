using Moj.CMS.Shared.Models;
using Moj.CMS.Shared.Requests;
using System;
using System.Linq.Expressions;

namespace Moj.CMS.Shared.Filter
{
    public class EntityHistoryFilter : Filter<EntityHistoryDto>
    {
        public string EntityId { get; set; }
        public string EntityName { get; set; }
        public string OperationType { get; set; }

        public override Expression<Func<EntityHistoryDto, bool>> ToExpression()
        {

            Expression<Func<EntityHistoryDto, bool>> expression = entity => (string.IsNullOrEmpty(EntityId) || entity.EntityId.Contains(EntityId)) &&
                                                                       (string.IsNullOrEmpty(EntityName) || entity.EntityName.Contains(EntityName)) &&
                                                                       (string.IsNullOrEmpty(OperationType) || entity.OperationType.Contains(OperationType));

            return expression;
        }
    }
}

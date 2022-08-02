using System;
using System.Linq.Expressions;
using Moj.CMS.Shared.Enums;
using Moj.CMS.Shared.Models;
using Moj.CMS.Shared.Requests;

namespace Moj.CMS.Shared.Filters
{
    public class LogFilter : Filter<Log>
    {
        public string RequestName { get; set; }
        public string InputDetails { get; set; }
        public string RequestId { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
        public RequestType? RequestType { get; set; }
        public override Expression<Func<Log, bool>> ToExpression()
        {
            Expression<Func<Log, bool>> expression = LogListItem => (string.IsNullOrEmpty(Status) || LogListItem.Status.Contains(Status))
             && (RequestType == null || LogListItem.RequestType == RequestType)
             && (string.IsNullOrEmpty(RequestName) || LogListItem.RequestName.Contains(RequestName))
             && (string.IsNullOrEmpty(RequestId) || LogListItem.RequestId.Contains(RequestId))
             && (string.IsNullOrEmpty(InputDetails) || LogListItem.InputDetails.Contains(InputDetails))
             && (string.IsNullOrEmpty(UserName) || LogListItem.UserName.Contains(UserName));

            return expression;
        }
    }
}

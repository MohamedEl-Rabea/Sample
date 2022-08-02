using Microsoft.AspNetCore.Http;
using Moj.CMS.Domain.Shared.Exceptions;
using System.Collections.Generic;

namespace Moj.CMS.Api.Validation
{
    public class DuplicateEntryProplemDetails : ProblemDetailsBase
    {
        public DuplicateEntryProplemDetails(DuplicateEntryException exception)
                : base(exception.RequestId)
        {
            Title = "Duplicate Entry";
            Status = StatusCodes.Status409Conflict;
            Type = "https://httpstatuses.com/409";
            Detail = exception.Message;
            Errors = new List<string> { exception.Message };
        }
    }
}

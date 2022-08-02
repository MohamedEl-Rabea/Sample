using Microsoft.AspNetCore.Http;
using Moj.CMS.Domain.Shared.Exceptions;
using System.Collections.Generic;

namespace Moj.CMS.Api.Validation
{
    public class BusinessRuleValidationExceptionProblemDetails : ProblemDetailsBase
    {
        public BusinessRuleValidationExceptionProblemDetails(BusinessRuleValidationException exception) : base(exception.RequestId)
        {
            Title = "Business rule broken";
            Status = StatusCodes.Status409Conflict;
            Detail = exception.Details;
            Errors = new List<string> { exception.Details };
            Type = "https://httpstatuses.com/409";
        }
    }
}

using Microsoft.AspNetCore.Http;
using Moj.CMS.Domain.Shared.Exceptions;
using System.Collections.Generic;

namespace Moj.CMS.Api.Validation
{
    public class ApplicationProblemDetails : ProblemDetailsBase
    {
        public ApplicationProblemDetails(CMSApplicationException exception)
            : base(exception.RequestId)
        {
            var innerException = exception.InnerException;
            Title = "Unhandled exception";
            Status = StatusCodes.Status500InternalServerError;
            Detail = innerException.Message;
            Errors = new List<string> { innerException.Message };
            Type = "https://httpstatuses.com/500";
        }
    }
}

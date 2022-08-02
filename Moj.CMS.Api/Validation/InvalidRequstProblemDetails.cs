using Microsoft.AspNetCore.Http;
using Moj.CMS.Shared.Exceptions;

namespace Moj.CMS.Api.Validation
{
    public class InvalidRequstProblemDetails : ProblemDetailsBase
    {
        public InvalidRequstProblemDetails()
        {

        }
        public InvalidRequstProblemDetails(InvalidRequestException exception) : base(exception.RequestId)
        {
            Title = "One or more validation errors occurred.";
            Status = StatusCodes.Status400BadRequest;
            Type = "https://httpstatuses.com/400";
            Errors = exception.Errors;
            Detail = exception.Message;
        }
    }
}

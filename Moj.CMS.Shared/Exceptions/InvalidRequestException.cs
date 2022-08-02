using Moj.CMS.Domain.Shared.Exceptions;
using System.Collections.Generic;
using System.Text.Json;
namespace Moj.CMS.Shared.Exceptions
{
    public class InvalidRequestException : AppExceptionBase
    {
        public List<string> Errors { get; }

        public InvalidRequestException(List<string> errors)
        {
            Errors = errors;
        }

        public override string GetDetails()
        {
            return JsonSerializer.Serialize(Errors);
        }
    }
}

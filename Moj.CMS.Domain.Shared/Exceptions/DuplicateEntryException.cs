using System;

namespace Moj.CMS.Domain.Shared.Exceptions
{
    public class DuplicateEntryException : AppExceptionBase
    {
        public DuplicateEntryException(string message)
            : base(message)
        {
        }
    }
}

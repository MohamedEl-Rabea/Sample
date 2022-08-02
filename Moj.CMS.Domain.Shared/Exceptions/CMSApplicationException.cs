using System;

namespace Moj.CMS.Domain.Shared.Exceptions
{
    public class CMSApplicationException : AppExceptionBase
    {
        public CMSApplicationException(Guid requestId, Exception exception)
            : base(requestId, exception)
        {
        }
    }
}

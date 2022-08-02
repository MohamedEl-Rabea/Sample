using System;

namespace Moj.CMS.Domain.Shared.Exceptions
{
    public class AppExceptionBase : Exception
    {
        public Guid RequestId { get; set; }

        public AppExceptionBase(Guid requestId, Exception exception)
            : base(null, exception)
        {
            RequestId = requestId;
        }
        public AppExceptionBase()
        {

        }
        public AppExceptionBase(string message) : base(message)
        {

        }

        public virtual string GetDetails()
        {
            return StackTrace;
        }
    }
}

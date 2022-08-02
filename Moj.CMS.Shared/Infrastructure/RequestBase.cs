using Moj.CMS.Shared.Enums;
using System;

namespace Moj.CMS.Shared.Infrastructure
{
    public abstract class RequestBase
    {
        public RequestBase()
        {
            RequestId = Guid.NewGuid();
            RequestName = GetType().Name;
        }
        public Guid RequestId { get; private set; }
        public string RequestName { get; private set; }
        public abstract RequestType RequestType { get; }
    }
}

using System;

namespace Moj.CMS.Integration.Contracts.Runtime
{
    public class ExecutionContext
    {
        public Guid RequestId { get; set; }
        public string UserId { get; set; }
    }
}

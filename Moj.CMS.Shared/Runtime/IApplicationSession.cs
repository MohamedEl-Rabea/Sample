using System;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Shared.Runtime
{
    [ScopedService]
    public interface IApplicationSession
    {
        string UserId { get; }
        public Guid RequestId { get; set; }
        public string RequestName { get; set; }
    }
}

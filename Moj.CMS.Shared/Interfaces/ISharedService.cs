using System;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Shared.Interfaces
{
    [ScopedService]
    public interface ISharedService
    {
        TResult Execute<TResult>(Func<TResult> func);
    }
}

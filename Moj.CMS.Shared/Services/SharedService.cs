using Microsoft.Extensions.DependencyInjection;
using Moj.CMS.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moj.CMS.Shared.Services
{
    public class SharedService : ISharedService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public SharedService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public TResult Execute<TResult>(Func<TResult> func)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var result = func.Invoke();
                return result;
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Moj.CMS.Modules.Integration.Application.Models;
using Moj.CMS.Shared.Infrastructure.Contexts;
using Moj.CMS.Shared.Runtime;

namespace Moj.CMS.Modules.Integration.Infrastructure.Contexts
{
    public class IntegrationDbContext : AuditedDbContext
    {
        public IntegrationDbContext(DbContextOptions<IntegrationDbContext> options, IApplicationSession applicationSession)
            : base(options, applicationSession)
        {
        }

    }
}

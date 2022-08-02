using Microsoft.EntityFrameworkCore;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Domain.Aggregates.Iban;
using Moj.CMS.Infrastructure.Contexts;
using Moj.CMS.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moj.CMS.Infrastructure.Queries
{
    public class IbanQueries:IIbanQueries
    {
        private readonly IQueryBuilderCreator<CMSDbContext> _queryBuilderCreator;

        public IbanQueries(IQueryBuilderCreator<CMSDbContext> queryBuilderCreator)
        {
            _queryBuilderCreator = queryBuilderCreator;
        }
        public async Task<int> GetIbanIdAsyc(string ibanNumber)
        {
            using var queryBuilder = _queryBuilderCreator.Create();
            var ibanId = await queryBuilder.Query<Iban>()
                .Where(i=>i.Number == ibanNumber).Select(i => i.Id).FirstOrDefaultAsync();
            return ibanId;
        }
    }
}

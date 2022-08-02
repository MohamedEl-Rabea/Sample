using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moj.CMS.Domain.Shared.Specifications;
using Moj.CMS.Shared.Requests;

namespace Moj.CMS.Shared.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<Wrapper.PagedResult<T>> ToPagedListAsync<T>(this IQueryable<T> source, PagedRequest<T> request)
            where T : class
        {
            if (source == null)
                throw new Exception("Source cannot be null");

            source = ApplyFilters(source, request);
            source = ApplySorting(source, request);

            var pageSize = request.PageSize;
            pageSize = pageSize == 0 ? 10 : pageSize;
            int count = await source.CountAsync();
            var pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber + 1;
            List<T> items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return Wrapper.PagedResult<T>.Success(items, count, pageNumber, pageSize);
        }

        private static IQueryable<T> ApplySorting<T>(IQueryable<T> source, PagedRequest<T> request) where T : class
        {
            if (request.Sort != null && request.Sort.Any())
            {
                request.Sort = request.Sort.OrderBy(s => s.Order);
                var sortStr = string.Join(',', request.Sort.Select(s => $"{s.Field} {s.Direction}"));
                return source.OrderBy(sortStr);
            }
            return source;
        }

        private static IQueryable<T> ApplyFilters<T>(IQueryable<T> source, PagedRequest<T> request)
            where T : class
        {
            var filter = request?.GetFilter();
            if (filter == null)
                return source;

            return  source.Where(filter.ToExpression());
        }

        public static Wrapper.PagedResult<T> ToPagedList<T>(this IEnumerable<T> source, PagedRequest<T> request) where T : class
        {
            if (source == null)
                throw new Exception("Source cannot be null");

            var pageNumber = request.PageNumber;
            var pageSize = request.PageSize;
            pageNumber = pageNumber == 0 ? 1 : pageNumber;
            pageSize = pageSize == 0 ? 10 : pageSize;
            int count = source.Count();
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            List<T> items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return Wrapper.PagedResult<T>.Success(items, count, pageNumber, pageSize);
        }

        public static IQueryable<T> Specify<T>(this IQueryable<T> query, ISpecification<T> spec) where T : class
        {
            var queryableResultWithIncludes = spec.Includes
                .Aggregate(query,
                    (current, include) => current.Include(include));
            var secondaryResult = spec.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));
            return secondaryResult.Where(spec.Criteria);
        }
    }
}
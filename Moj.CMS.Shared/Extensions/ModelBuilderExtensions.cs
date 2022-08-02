using Microsoft.EntityFrameworkCore;
using Moj.CMS.Domain.Shared.Audit;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Moj.CMS.Shared.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ApplyGlobalFilters(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var filterExpression = typeof(ModelBuilderExtensions)
                    .GetMethod(nameof(ModelBuilderExtensions.CreateFilterExpression), BindingFlags.NonPublic | BindingFlags.Static)
                    .MakeGenericMethod(entityType.ClrType)
                    .Invoke(null, new object[] { });

                entityType.SetQueryFilter((LambdaExpression)filterExpression);
            }
        }

        private static Expression<Func<TEntity, bool>> CreateFilterExpression<TEntity>()
            where TEntity : class
        {
            Expression<Func<TEntity, bool>> expression = null;

            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
                expression = e => !EF.Property<bool>(e, "IsDeleted");

            return expression;
        }

    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Moj.CMS.Infrastructure.Extensions
{
    public static class IdentityInsertHelpers
    {
        public static Task EnableIdentityInsert<T>(this DbContext context) => SetIdentityInsertAsync<T>(context, true);
        public static Task DisableIdentityInsert<T>(this DbContext context) => SetIdentityInsertAsync<T>(context, false);

        private static async Task SetIdentityInsertAsync<T>([NotNull] DbContext context, bool enable)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            var entityType = context.Model.FindEntityType(typeof(T));
            var value = enable ? "ON" : "OFF";
            var rawSqlCommand = $"SET IDENTITY_INSERT {entityType.GetSchema()}.{entityType.GetTableName()} {value}";
            await context.Database.ExecuteSqlRawAsync(rawSqlCommand);
        }

        public static async Task SaveChangesWithIdentityInsertAsync<T>([NotNull] this DbContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            await context.Database.OpenConnectionAsync();
            try
            {
                await context.EnableIdentityInsert<T>();
                await context.SaveChangesAsync();
                await context.DisableIdentityInsert<T>();
            }
            finally
            {
                await context.Database.CloseConnectionAsync();
            }
        }
    }
}

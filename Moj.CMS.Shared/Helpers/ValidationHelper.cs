using System;
using System.Collections.Generic;
using System.Linq;

namespace Moj.CMS.Shared.Helpers
{
    public static class ValidationHelper
    {
        public static bool ListItemsAreUnique<T>(IEnumerable<T> lst)
        {
            return lst != null && lst.Distinct().Count() == lst.Count();
        }
        public static bool ListItemsAreUnique<T>(IEnumerable<T> lst, IEqualityComparer<T> comparer)
        {
            return lst != null && lst.Distinct(comparer).Count() == lst.Count();
        }
        public static bool ListNotEmpty(IEnumerable<object> lst)
        {
            return lst != null && lst.Count() > 0;
        }
        public static bool ValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
        public static bool ValidOptionalDate(DateTime? date)
        {
            return !date.HasValue || !date.Equals(default(DateTime));
        }
        // validate string as date
        private static bool ValidDate(string value)
        {
            DateTime date;
            return DateTime.TryParse(value, out date);
        }
    }
}

using System;
using System.Globalization;

namespace Moj.CMS.Shared.Extensions
{
    public static class DateExtensions
    {
        public static TimeZoneInfo TimeZone { get; set; }

        public static string FormatDate(this DateTime input)
        {
            return input.GetClientTime().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        }
        public static string FormatDateTime(this DateTime input)
        {
            return input.GetClientTime().ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
        }

        public static DateTime GetClientTime(this DateTime input)
        {
            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(
                DateTime.SpecifyKind(input, DateTimeKind.Utc),
                TimeZone.StandardName);
        }
    }
}

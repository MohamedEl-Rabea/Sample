using System.Globalization;
using System.Threading;
using OfficeOpenXml;

namespace Moj.CMS.Shared.Helpers
{
    public static class ExcelHelper
    {
        public static bool IsRightToLeft()
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            return cultureInfo.TwoLetterISOLanguageName == "ar";
        }
        public static void SetHeaderNames(ref ExcelWorksheet worksheet, params string[] names)
        {
            for (int i = 1; i <= names.Length; i++)
            {
                worksheet.Cells[1, i].Value = names[i - 1];
            }
        }
    }
}

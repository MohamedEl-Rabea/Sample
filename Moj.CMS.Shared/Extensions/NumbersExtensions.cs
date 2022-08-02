using System.Linq;
using System.Threading;

namespace Moj.CMS.Shared.Extensions
{
    public static class NumbersExtensions
    {
        public static string FormatZeroDecimals(this decimal input)
        {
            return input.ToString("G29");
        }

        public static object LocalizeNumbers_Deprecated(this object input)
        {
            var inputStr = input.ToString();
            if (new string[] { "ar-EG", "ar-SA" }.Contains(Thread.CurrentThread.CurrentCulture.Name))
            {
                return inputStr.Replace('0', '\u0660')
                        .Replace('1', '\u0661')
                        .Replace('2', '\u0662')
                        .Replace('3', '\u0663')
                        .Replace('4', '\u0664')
                        .Replace('5', '\u0665')
                        .Replace('6', '\u0666')
                        .Replace('7', '\u0667')
                        .Replace('8', '\u0668')
                        .Replace('9', '\u0669');
            }
            else
                return input;
        }
    }
}

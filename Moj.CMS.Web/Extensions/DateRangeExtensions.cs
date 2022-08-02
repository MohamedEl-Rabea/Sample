using Moj.CMS.Shared.DTO;
using MudBlazor;
using SSS.Components.NumberRange;

namespace Moj.CMS.Web.Extensions
{
    public static class RangeExtensions
    {
        public static DateRangeDto MapToDateRangeDto(this DateRange dateRange)
        {
            return dateRange?.Start != null
           ? new DateRangeDto { From = dateRange.Start.Value, To = dateRange.End }
           : new DateRangeDto();
        }

        public static NumberRangeDto MapToNumberRangeDto(this DecimalRange decimalRange)
        {
            return new NumberRangeDto { Min = decimalRange?.Start, Max = decimalRange?.End };
        }
    }
}

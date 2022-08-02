using Moj.CMS.Domain.DomainServices;
using Moj.CMS.Domain.Shared.Entities;
using System.Threading.Tasks;

namespace Moj.CMS.Domain.Aggregates.Case.BusinessRules
{
    public class CourtDetailsMustBeValidBusniessRule : IAsyncBusinessRule
    {
        private readonly IGetDivisionCourtCode _getDivisionCourtCode;
        private readonly IEnforceCourtIsExists _enforceCourtIsExists;
        private readonly string _courtCode;
        private readonly string _divisionCode;

        public CourtDetailsMustBeValidBusniessRule(string courtCode, string divisionCode,
                                                   IEnforceCourtIsExists enforceCourtIsExists,
                                                   IGetDivisionCourtCode getDivisionCourtCode)
        {
            _getDivisionCourtCode = getDivisionCourtCode;
            _enforceCourtIsExists = enforceCourtIsExists;
            _courtCode = courtCode;
            _divisionCode = divisionCode;
        }
        public string Message { get; private set; }

        public async Task<bool> IsBrokenAsync()
        {
            var isCourtExist = await _enforceCourtIsExists.IsExistAsync(_courtCode);
            if (!isCourtExist)
            {
                Message = $"Court with Code {_courtCode} not found";
                return true;
            }
            var divisionCourtCode = await _getDivisionCourtCode.GetDivisionCourtCodeAsync(_divisionCode);
            if (divisionCourtCode == null)
            {
                Message = $"Division with Code {_divisionCode} not found";
                return true;
            }
            if (divisionCourtCode != _courtCode)
            {
                Message = $"Division with Code {_divisionCode} not related to court with code {_courtCode}";
                return true;
            }
            return false;
        }
    }
}

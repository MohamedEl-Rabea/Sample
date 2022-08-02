using Moj.CMS.Domain.DomainServices;
using Moj.CMS.Domain.Shared.Entities;
using System.Threading.Tasks;

namespace Moj.CMS.Domain.Aggregates.Case.BusinessRules
{
    public class JudgeMustBeExistsBusniessRule : IAsyncBusinessRule
    {
        private readonly IEnforceJudgeIsExists _enforceJudgeIsExists;
        private readonly string _judgeCode;

        public JudgeMustBeExistsBusniessRule(string judgeCode,IEnforceJudgeIsExists enforceJudgeIsExists)
        {
            _judgeCode = judgeCode;
            _enforceJudgeIsExists = enforceJudgeIsExists;
        }

        public string Message => $"Judge with Code {_judgeCode} not found";

        public async Task<bool> IsBrokenAsync()
        {
            var isFound = await _enforceJudgeIsExists.IsExistAsync(_judgeCode);
            return !isFound;
        }
    }
}

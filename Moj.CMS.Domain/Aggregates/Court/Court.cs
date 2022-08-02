using Moj.CMS.Domain.Aggregates.Court.Entities;
using Moj.CMS.Domain.Aggregates.Court.ValueObjects;
using Moj.CMS.Domain.ParameterObjects.Court;
using Moj.CMS.Domain.Shared.Audit;
using Moj.CMS.Domain.Shared.Entities;
using Moj.CMS.Domain.Shared.Guard;
using System.Collections.Generic;
using System.Linq;

namespace Moj.CMS.Domain.Aggregates.Court
{
    public class Court : FullAuditedAggregateRoot, IAggregateRoot
    {
        private Court()
        {
            _divisions = new List<Division>();
            _banckAccounts = new List<CourtBankAccount>();
        }

        public string Name { get; private set; }
        public string Code { get; private set; }
        public string AreaCode { get; private set; }
        public bool IsActive { get; private set; }

        public IReadOnlyCollection<Division> Divisions => _divisions.ToList();
        private readonly List<Division> _divisions;

        public IReadOnlyCollection<CourtBankAccount> BankAccounts => _banckAccounts.ToList();
        private readonly List<CourtBankAccount> _banckAccounts;

        public static Court Create(CourtInfoParam param)
        {
            //TODO:Rules to be added
            //1- Court have only one active account
            //2- Area code should be valid and exists
            var court = new Court();
            court.SetBasicDetials(param);
            court.AddDivisions(param.Divisions);
            court._banckAccounts.AddRange(param.BanckAccounts);
            return court;
        }

        public void Update(CourtInfoParam param)
        {
            SetBasicDetials(param);
            AddDivisions(param.Divisions);
            _banckAccounts.AddRange(param.BanckAccounts);
        }

        private void SetBasicDetials(CourtInfoParam param)
        {
            Guard.AssertArgumentNotNullOrEmptyOrWhitespace(param.Name, nameof(param.Name));
            Guard.AssertArgumentNotNullOrEmptyOrWhitespace(param.Code, nameof(param.Code));
            Guard.AssertArgumentNotNullOrEmptyOrWhitespace(param.AreaCode, nameof(param.AreaCode));

            Name = param.Name;
            Code = param.Code;
            AreaCode = param.AreaCode;
            IsActive = param.IsActive;
        }

        public void AddDivisions(IEnumerable<Division> divisions)
        {
            if (divisions != null)
                _divisions.AddRange(divisions);
        }

        public void ReplaceDivisions(IEnumerable<Division> divisions)
        {
            _divisions.RemoveAll(d => divisions.Any(input => input.Code == d.Code));
            _divisions.AddRange(divisions);
        }
    }
}
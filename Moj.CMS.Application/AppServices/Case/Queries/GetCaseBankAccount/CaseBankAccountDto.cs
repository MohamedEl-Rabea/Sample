using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Case.Queries.GetCaseBankAccount
{
    public class CaseBankAccountDto
    {
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public string ReferenceType { get; set; }
        public string ReferenceName { get; set; }
        public string CollectionAccount { get; set; }
        public int TransactionCount { get; set; }
        public string AccountName { get; set; }
        public string BankName { get; set; }
    }
}

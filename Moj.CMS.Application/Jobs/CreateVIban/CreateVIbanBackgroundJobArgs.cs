using Moj.CMS.Domain.Shared.Enums;

namespace Moj.CMS.Application.Jobs.CreateVIban
{
    public class CreateVIbanBackgroundJobArgs
    {
        public string ParentAccount { get; set; }
        public decimal CAP { get; set; }
        public string Alias { get; set; }
        public string ReferenceNumber { get; set; }
        public VIbanReferenceTypeEnum ReferenceType { get; set; }
    }
}

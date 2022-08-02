using Moj.CMS.Domain.Shared.Values;

namespace Moj.CMS.Application.AppServices.VIban.Dtos
{
    public class CreateVIbanDto
    {
        public string ParentAccount { get; set; }
        public string Alias { get; set; }
        public decimal CAP { get; set; }
        public VIbanReferenceDetails ReferenceDetails { get; set; }
    }
}

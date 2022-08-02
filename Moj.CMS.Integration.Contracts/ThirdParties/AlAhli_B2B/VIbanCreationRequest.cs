namespace Moj.CMS.Integration.Contracts.AlAhli_B2B
{
    public class VIbanCreationRequest
    {
        public string ParentAccount { get; set; }
        public string Alias { get; set; }
        public decimal Cap { get; set; }
    }
}
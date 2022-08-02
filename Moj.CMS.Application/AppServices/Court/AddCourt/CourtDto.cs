namespace Moj.CMS.Application.AppServices.Court
{
    public class CourtDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string AreaCode { get; set; }
        public string[] BankAccounts { get; set; }
        public bool IsActive { get; set; }
    }
}

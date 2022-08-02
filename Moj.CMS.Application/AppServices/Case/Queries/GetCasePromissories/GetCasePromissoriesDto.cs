namespace Moj.CMS.Application.AppServices.Case.Queries.GetCasePromissories
{
    public class GetCasePromissoriesDto
    {
        public string PromissoryTypeName { get; set; }
        public string PromissoryTypeDesription { get; set; }
        public int PromissoryId { get; set; }
        public int PromissoryTypeId { get; set; }
        public int Count { get; set; }
        public string PromissoriesNumbers { get; set; }
    }
}

namespace Moj.CMS.Shared.Requests
{
    public class Sort
    {
        public string Field { get; set; }
        public int Order { get; set; }
        public SortDirection Direction { get; set; }
    }

    public enum SortDirection
    {
        asc,
        desc
    }
}

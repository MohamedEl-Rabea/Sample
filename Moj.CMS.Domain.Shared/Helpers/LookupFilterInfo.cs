namespace Moj.CMS.Domain.Shared.Helpers
{
    public class LookupFilterInfo
    {
        public int Order { get; set; }
        public string Name { get; set; }
        public PropertyType PropertyType { get; set; }
    }

    public class LookupFilterValue
    {
        public PropertyType PropertyType { get; set; }
        public object Value { get; set; }
    }

    public enum PropertyType
    {
        String,
        Digits,
        Status
    }
}

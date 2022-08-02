namespace Moj.CMS.Domain.ParameterObjects
{
    public class ProductAggregateCreationParam
    {
        public string Name { get; set; }

        public string Barcode { get; set; }

        public string Description { get; set; }

        public string Brand { get; set; }

        //Any other validation services or cross-aggregate service to fullfil aggregate info
    }
}

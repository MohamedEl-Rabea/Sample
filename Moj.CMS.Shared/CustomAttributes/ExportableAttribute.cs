using System;

namespace Moj.CMS.Shared.CustomAttributes
{
    public class ExportableAttribute : Attribute
    {
        public string Name { get; set; }
        public int Order { get; set; }
        public ExportableAttribute()
        {

        }
    }
}

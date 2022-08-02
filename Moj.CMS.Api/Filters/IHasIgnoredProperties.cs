using System.Collections.Generic;

namespace Moj.CMS.Api.Filters
{
    public interface IHasIgnoredProperties
    {
        IEnumerable<string> GetIgnoredProperties();
    }
}

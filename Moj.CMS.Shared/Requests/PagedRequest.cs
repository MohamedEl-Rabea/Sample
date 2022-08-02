using Newtonsoft.Json;
using System.Collections.Generic;

namespace Moj.CMS.Shared.Requests
{
    public class PagedRequest<T>
    {
        private string _currentState;

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public dynamic Filter { get; set; }
        public IEnumerable<Sort> Sort { get; set; }

        private void RefreshCurrentState()
        {
            _currentState = JsonConvert.SerializeObject(this);
        }
        public bool StateHasChanged()
        {
            var newState = JsonConvert.SerializeObject(this);
            var changed = _currentState != newState;
            if (changed)
                RefreshCurrentState();

            return changed;
        }
        public virtual IFilter<T> GetFilter() { return Filter; }
       
    }
}

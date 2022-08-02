using System;
using System.Linq;
using System.Linq.Expressions;

namespace Moj.CMS.Shared.Requests
{
    public abstract class Filter<T> : IFilter<T>
    {
        public virtual void Clear() 
        {
            GetType().GetProperties().ToList().ForEach(prop => prop.SetValue(this, null));
        }

        public abstract Expression<Func<T, bool>> ToExpression();
    }
}

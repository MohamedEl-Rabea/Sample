using System;
using System.Linq.Expressions;

namespace Moj.CMS.Shared.Requests
{
    public interface IFilter<T>
    {
        Expression<Func<T, bool>> ToExpression();
    }
}

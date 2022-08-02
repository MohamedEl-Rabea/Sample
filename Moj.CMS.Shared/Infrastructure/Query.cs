using MediatR;
using Moj.CMS.Shared.Enums;

namespace Moj.CMS.Shared.Infrastructure
{
    public abstract class Query<TResult> : RequestBase, IRequest<TResult>
    {
        public override RequestType RequestType { get => RequestType.Query; }
    }
}

using MediatR;
using Moj.CMS.Shared.Enums;

namespace Moj.CMS.Shared.Infrastructure
{
    public abstract class Command : RequestBase, IRequest
    {
        public override RequestType RequestType { get => RequestType.Command; }
    }

    public abstract class Command<TResult> : RequestBase, IRequest<TResult>
    {
        public override RequestType RequestType { get => RequestType.Command; }
    }
}
using ECommerceManagement.API.Data;
using MediatR;

namespace ECommerceManagement.API.Handlers
{
    public abstract class BaseHandler<TReq> : IRequestHandler<TReq>
        where TReq : IRequest
    {
        protected readonly EcommerceContext _context;

        public BaseHandler(EcommerceContext context)
        {
            _context = context;
        }

        public virtual Task Handle(TReq request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    public abstract class BaseHandler<TReq, TResp> : IRequestHandler<TReq, TResp>
        where TReq : IRequest<TResp>
    {
        protected readonly EcommerceContext _context;

        public BaseHandler(EcommerceContext context)
        {
            _context = context;
        }

        public virtual Task<TResp> Handle(TReq request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

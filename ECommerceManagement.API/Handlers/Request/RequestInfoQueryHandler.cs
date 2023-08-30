using ECommerceManagement.API.Data;
using ECommerceManagement.API.DTOs;
using ECommerceManagement.API.Exceptions;
using ECommerceManagement.API.Queries.Request;
using Microsoft.EntityFrameworkCore;

namespace ECommerceManagement.API.Handlers.Request
{
    public class RequestInfoQueryHandler : BaseHandler<RequestInfoQuery, RequestInfoObject>
    {
        public RequestInfoQueryHandler(EcommerceContext context) : base(context) { }

        public override async Task<RequestInfoObject> Handle(RequestInfoQuery request, CancellationToken cancellationToken)
        {
            var req = await _context.Requests
                .Include(r => r.Product)
                .Include(r => r.Shop)
                .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException($"Request with the id {request.Id} not found");

            return new RequestInfoObject
            {
                Id = req.Id,
                ManagerId = req.ManagerId,
                ProductId = req.ProductId,
                ShopId = req.ShopId,
                Amount = req.Amount,
                Status = req.Status,
                ProductName = req.Product.Name,
                ShopName = req.Shop.IdNavigation.Name,
            };
        }
    }
}

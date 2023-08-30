using ECommerceManagement.API.Data;
using ECommerceManagement.API.DTOs;
using ECommerceManagement.API.Extensions;
using ECommerceManagement.API.Queries.Request;
using Microsoft.EntityFrameworkCore;

namespace ECommerceManagement.API.Handlers.Request
{
    public class ManagerRequestsQueryHandler : BaseHandler<ManagerRequestsQuery, ListObject<RequestInfoObject>>
    {
        public ManagerRequestsQueryHandler(EcommerceContext context) : base(context) { }

        public override async Task<ListObject<RequestInfoObject>> Handle(ManagerRequestsQuery request, CancellationToken cancellationToken)
        {
            var requests = _context.Requests
                .Where(req => req.ManagerId == request.ManagerId)
                .Where(req => req.Status == request.Status)
                .Include(req => req.Product)
                .Include(req => req.Shop)
                .OrderBy(req => req.Product.Name);

            var total = await requests.CountAsync(cancellationToken);
            var items = await requests
                .Paginate(request.Pagination.Offset, request.Pagination.Limit)
                .Select(req => new RequestInfoObject
                {
                    Id = req.Id,
                    ShopId = req.ShopId,
                    ShopName = req.Shop.IdNavigation.Name,
                    ManagerId = req.ManagerId,
                    ProductId = req.ProductId,
                    ProductName = req.Product.Name,
                    Amount = req.Amount,
                    Status = req.Status,
                })
                .ToListAsync(cancellationToken);
            
            return new ListObject<RequestInfoObject>
            {
                Offset = request.Pagination.Offset,
                Limit = request.Pagination.Limit,
                Total = total,
                Items = items,
            };
        }
    }
}

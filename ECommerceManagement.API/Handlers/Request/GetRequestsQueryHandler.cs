using ECommerceManagement.API.Data;
using ECommerceManagement.API.DTOs;
using ECommerceManagement.API.Extensions;
using ECommerceManagement.API.Queries.Request;
using Microsoft.EntityFrameworkCore;

namespace ECommerceManagement.API.Handlers.Request
{
    public class GetRequestsQueryHandler : BaseHandler<GetRequestsQuery, ListObject<RequestInfoObject>>
    {
        public GetRequestsQueryHandler(EcommerceContext context) : base(context)
        {
        }

        public override async Task<ListObject<RequestInfoObject>> Handle(GetRequestsQuery request, CancellationToken cancellationToken)
        {
            var requests = _context.Requests.AsNoTracking()
                .Where(r => r.Status == request.Status)
                .Include(r => r.Shop)
                .Include(r => r.Manager)
                .OrderBy(r => r.Product.Name);

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

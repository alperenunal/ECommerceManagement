using ECommerceManagement.API.Data;
using ECommerceManagement.API.DTOs;
using ECommerceManagement.API.Extensions;
using ECommerceManagement.API.Queries.Stock;
using Microsoft.EntityFrameworkCore;

namespace ECommerceManagement.API.Handlers.Stock
{
    public class WarehouseStocksQueryHandler : BaseHandler<WarehouseStocksQuery, ListObject<StockInfoObject>>
    {
        public WarehouseStocksQueryHandler(EcommerceContext context) : base(context)
        {
        }

        public override async Task<ListObject<StockInfoObject>> Handle(WarehouseStocksQuery request, CancellationToken cancellationToken)
        {
            var stocks = _context.Stocks.AsNoTracking()
                .Include(s => s.Product)
                .Where(s => s.StoreId == null)
                .Where(s => s.Product.CategoryId == request.CategoryId)
                .OrderBy(s => s.Product.Name);

            var total = await stocks.CountAsync(cancellationToken);

            var items = await stocks.AsNoTracking()
                .Paginate(request.Pagination.Offset, request.Pagination.Limit)
                .Select(s => new StockInfoObject
                {
                    Id = s.Id,
                    ProductId = s.ProductId,
                    ProductName = s.Product.Name,
                    Amount = s.Amount,
                    StoreId = 0,
                    StoreName = "",
                })
                .ToListAsync(cancellationToken);

            return new ListObject<StockInfoObject>
            {
                Offset = request.Pagination.Offset,
                Limit = request.Pagination.Limit,
                Total = total,
                Items = items,
            };
        }
    }
}

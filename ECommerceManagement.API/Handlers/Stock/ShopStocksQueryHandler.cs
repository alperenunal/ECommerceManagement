using ECommerceManagement.API.Data;
using ECommerceManagement.API.DTOs;
using ECommerceManagement.API.Extensions;
using ECommerceManagement.API.Queries.Stock;
using Microsoft.EntityFrameworkCore;

namespace ECommerceManagement.API.Handlers.Stock
{
    public class ShopStocksQueryHandler : BaseHandler<ShopStocksQuery, ListObject<StockInfoObject>>
    {
        public ShopStocksQueryHandler(EcommerceContext context) : base(context) { }

        public override async Task<ListObject<StockInfoObject>> Handle(ShopStocksQuery request, CancellationToken cancellationToken)
        {
            var stock = _context.Stocks.AsNoTracking()
                .Include(stock => stock.Product)
                .Include(stock => stock.Store)
                .Where(stock => stock.StoreId == request.ShopId)
                .Where(stock => stock.Store!.StoreType == false)
                .OrderBy(stock => stock.Product.Name);

            var total = await stock.CountAsync(cancellationToken);

            var items = await stock.AsNoTracking()
                .Paginate(request.Pagination.Offset, request.Pagination.Limit)
                .Select(stock => new StockInfoObject
                {
                    Id = stock.Id,
                    StoreId = stock.StoreId,
                    ProductId = stock.ProductId,
                    Amount = stock.Amount,
                    StoreName = stock.Store!.Name,
                    ProductName = stock.Product.Name,
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

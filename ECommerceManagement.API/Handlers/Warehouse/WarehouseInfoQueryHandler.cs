using ECommerceManagement.API.Data;
using ECommerceManagement.API.DTOs;
using ECommerceManagement.API.Extensions;
using ECommerceManagement.API.Queries.Warehouse;
using Microsoft.EntityFrameworkCore;

namespace ECommerceManagement.API.Handlers.Warehouse
{
    public class WarehouseInfoQueryHandler : BaseHandler<WarehouseInfoQuery, WarehouseInfoObject>
    {
        public WarehouseInfoQueryHandler(EcommerceContext context) : base(context) { }

        public override async Task<WarehouseInfoObject> Handle(WarehouseInfoQuery request, CancellationToken cancellationToken)
        {
            var warehouse = await _context.Stores.AsNoTracking()
                .Where(s => s.StoreType == true)
                .Include(s => s.AddressNavigation)
                .Include(s => s.Stocks)
                .FirstAsync(cancellationToken);

            var total = warehouse.Stocks.Count;
            var items = warehouse.Stocks
                        .Paginate(request.Pagination.Offset, request.Pagination.Limit)
                        .OrderBy(s => s.Product.Name)
                        .Select(s => new StockInfoObject
                        {
                            Id = s.Id,
                            ProductId = s.ProductId,
                            ProductName = s.Product.Name,
                            StoreId = s.StoreId,
                            StoreName = s.Store!.Name,
                            Amount = s.Amount,
                        })
                        .ToList();

            return new WarehouseInfoObject
            {
                Id = warehouse.Id,
                Name = warehouse.Name,
                AddressInfo = new AddressInfoObject
                {
                    Id = warehouse.AddressNavigation.Id,
                    Address = warehouse.AddressNavigation.Address1,
                    City = warehouse.AddressNavigation.City,
                    Country = warehouse.AddressNavigation.Country,
                    Zipcode = warehouse.AddressNavigation.Zipcode,
                },
                StockInfo = new ListObject<StockInfoObject>
                {
                    Offset = request.Pagination.Offset,
                    Limit = request.Pagination.Limit,
                    Total = total,
                    Items = items,
                },
            };
        }
    }
}

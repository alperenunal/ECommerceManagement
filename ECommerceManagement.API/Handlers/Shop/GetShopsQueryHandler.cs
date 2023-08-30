using ECommerceManagement.API.Data;
using ECommerceManagement.API.DTOs;
using ECommerceManagement.API.Extensions;
using ECommerceManagement.API.Queries.Shop;
using Microsoft.EntityFrameworkCore;

namespace ECommerceManagement.API.Handlers.Shop
{
    public class GetShopsQueryHandler : BaseHandler<GetShopsQuery, ListObject<ShopInfoObject>>
    {
        public GetShopsQueryHandler(EcommerceContext context) : base(context)
        {
        }

        public override async Task<ListObject<ShopInfoObject>> Handle(GetShopsQuery request, CancellationToken cancellationToken)
        {
            var total = await _context.Shops.CountAsync(cancellationToken);

            var items = await _context.Stores.AsNoTracking()
                .Where(s => s.StoreType == false)
                .Include(s => s.Shop)
                .Include(s => s.AddressNavigation)
                .Paginate(request.Pagination.Offset, request.Pagination.Limit)
                .Select(store => new ShopInfoObject
                {
                    Id = store.Id,
                    Name = store.Name,
                    Area = store.Shop!.Area,
                    AddressInfo = new AddressInfoObject
                    {
                        Id = store.Address,
                        Address = store.AddressNavigation.Address1,
                        City = store.AddressNavigation.City,
                        Country = store.AddressNavigation.Country,
                        Zipcode = store.AddressNavigation.Zipcode,
                    }
                })
                .ToListAsync(cancellationToken);

            return new ListObject<ShopInfoObject>
            {
                Offset = request.Pagination.Offset,
                Limit = request.Pagination.Limit,
                Total = total,
                Items = items,
            };
        }
    }
}

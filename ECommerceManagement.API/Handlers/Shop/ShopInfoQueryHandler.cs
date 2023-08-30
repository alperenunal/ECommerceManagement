using ECommerceManagement.API.Data;
using ECommerceManagement.API.DTOs;
using ECommerceManagement.API.Queries.Shop;
using ECommerceManagement.API.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ECommerceManagement.API.Handlers.Shop
{
    public class ShopInfoQueryHandler : BaseHandler<ShopInfoQuery, ShopInfoObject>
    {
        public ShopInfoQueryHandler(EcommerceContext context) : base(context) { }

        public override async Task<ShopInfoObject> Handle(ShopInfoQuery request, CancellationToken cancellationToken)
        {
            var store = await _context.Stores.AsNoTracking()
                .Where(store => store.StoreType == false)
                .Include(store => store.Shop)
                .Include(store => store.AddressNavigation)
                .Where(store => store.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new NotFoundException($"Store {request.Id} not found");

            return new ShopInfoObject
            {
                Id = store.Id,
                Name = store.Name,
                Area = store.Shop!.Area,
                AddressInfo = new AddressInfoObject
                {
                    Id = store.AddressNavigation.Id,
                    Address = store.AddressNavigation.Address1,
                    City = store.AddressNavigation.City,
                    Country = store.AddressNavigation.Country,
                    Zipcode = store.AddressNavigation.Zipcode,
                }
            };
        }
    }
}

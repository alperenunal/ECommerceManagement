using ECommerceManagement.API.Commands.Shop;
using ECommerceManagement.API.Data;
using Geo.MapBox.Abstractions;
using Geo.MapBox.Models.Parameters;

namespace ECommerceManagement.API.Handlers.Shop
{
    public class CreateShopCommandHandler : BaseHandler<CreateShopCommand>
    {
        private readonly IMapBoxGeocoding _geocoding;

        public CreateShopCommandHandler(EcommerceContext context, IMapBoxGeocoding geocoding) : base(context)
        {
            _geocoding = geocoding;
        }

        public override async Task Handle(CreateShopCommand request, CancellationToken cancellationToken)
        {
            var geo = await _geocoding.GeocodingAsync(new GeocodingParameters
            {
                Query = request.Address.Address,
            }, cancellationToken);
            var center = geo.Features.First().Center;

            var address = new Models.Address
            {
                Address1 = request.Address.Address,
                Country = request.Address.Country,
                City = request.Address.City,
                Zipcode = request.Address.Zipcode,
                CustomerId = null,
                Coordinates = new NpgsqlTypes.NpgsqlPoint(center.Latitude, center.Longitude), 
            };

            await _context.Addresses.AddAsync(address, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var store = new Models.Store
            {
                Id = request.Id,
                Name = request.Name,
                StoreType = false,
                Address = address.Id,
            };

            await _context.Stores.AddAsync(store, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var shop = new Models.Shop
            {
                Id = store.Id,
                Area = request.Area,
            };

            await _context.Shops.AddAsync(shop, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

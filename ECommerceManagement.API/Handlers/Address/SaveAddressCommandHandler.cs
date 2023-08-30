using ECommerceManagement.API.Commands.Address;
using ECommerceManagement.API.Data;
using Geo.MapBox.Abstractions;
using Geo.MapBox.Models.Parameters;

namespace ECommerceManagement.API.Handlers.Address
{
    public class SaveAddressCommandHandler : BaseHandler<SaveAddressCommand>
    {
        private readonly IMapBoxGeocoding _geocoding;

        public SaveAddressCommandHandler(EcommerceContext context, IMapBoxGeocoding geocoding) : base(context)
        {
            _geocoding = geocoding;
        }

        public override async Task Handle(SaveAddressCommand request, CancellationToken cancellationToken)
        {
            var coordinate = await _geocoding.GeocodingAsync(new GeocodingParameters
            {
                Query = request.Address,
            }, cancellationToken);
            var center = coordinate.Features.First().Center;

            var address = new Models.Address
            {
                CustomerId = request.CustomerId,
                Address1 = request.Address,
                Zipcode = request.Zipcode,
                City = request.City,
                Country = request.Country,
                Coordinates = new NpgsqlTypes.NpgsqlPoint(center.Latitude, center.Longitude),
            };

            await _context.Addresses.AddAsync(address, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

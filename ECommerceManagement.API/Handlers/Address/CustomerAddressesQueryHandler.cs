using ECommerceManagement.API.Data;
using ECommerceManagement.API.DTOs;
using ECommerceManagement.API.Extensions;
using ECommerceManagement.API.Queries.Address;
using Microsoft.EntityFrameworkCore;

namespace ECommerceManagement.API.Handlers.Address
{
    public class CustomerAddressesQueryHandler : BaseHandler<CustomerAddressesQuery, ListObject<AddressInfoObject>>
    {
        public CustomerAddressesQueryHandler(EcommerceContext context) : base(context)
        {
        }

        public override async Task<ListObject<AddressInfoObject>> Handle(CustomerAddressesQuery request, CancellationToken cancellationToken)
        {
            var count = await _context.Addresses.AsNoTracking()
                .CountAsync(a => a.CustomerId == request.Id, cancellationToken);

            var items = await _context.Addresses.AsNoTracking()
                .Where(a => a.CustomerId == request.Id)
                .Select(a => new AddressInfoObject
                {
                    Id= a.Id,
                    Address = a.Address1,
                    Zipcode = a.Zipcode,
                    City = a.City,
                    Country = a.Country,
                })
                .Paginate(request.Pagination.Offset, request.Pagination.Limit)
                .ToListAsync(cancellationToken);

            return new ListObject<AddressInfoObject>
            {
                Offset = request.Pagination.Offset,
                Limit = request.Pagination.Limit,
                Total = count,
                Items = items
            };
        }
    }
}

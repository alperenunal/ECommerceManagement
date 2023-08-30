using ECommerceManagement.API.DTOs;
using MediatR;

namespace ECommerceManagement.API.Queries.Address
{
    public class CustomerAddressesQuery : IRequest<ListObject<AddressInfoObject>>
    {
        public Guid Id { get; set; }
        public PaginationObject Pagination { get; set; } = null!;
    }
}

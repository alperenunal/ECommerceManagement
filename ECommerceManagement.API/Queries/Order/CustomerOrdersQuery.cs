using ECommerceManagement.API.DTOs;
using MediatR;

namespace ECommerceManagement.API.Queries.Order
{
    public class CustomerOrdersQuery : IRequest<ListObject<OrderInfoObject>>
    {
        public Guid Id { get; set; }
        public PaginationObject Pagination { get; set; } = null!;
    }
}

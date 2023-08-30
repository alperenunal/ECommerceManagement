using ECommerceManagement.API.DTOs;
using MediatR;

namespace ECommerceManagement.API.Queries.Order
{
    public class ShopOrdersQuery : IRequest<ListObject<OrderInfoObject>>
    {
        public int Id { get; set; }
        public PaginationObject Pagination { get; set; } = null!;
    }
}

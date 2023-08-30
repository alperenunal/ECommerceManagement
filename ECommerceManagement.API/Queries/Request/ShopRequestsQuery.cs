using ECommerceManagement.API.DTOs;
using MediatR;

namespace ECommerceManagement.API.Queries.Request
{
    public class ShopRequestsQuery : IRequest<ListObject<RequestInfoObject>>
    {
        public int Id { get; set; }
        public bool? Status { get; set; }
        public PaginationObject Pagination { get; set; } = null!;
    }
}

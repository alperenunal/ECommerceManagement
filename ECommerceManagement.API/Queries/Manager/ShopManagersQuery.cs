using ECommerceManagement.API.DTOs;
using MediatR;

namespace ECommerceManagement.API.Queries.Manager
{
    public class ShopManagersQuery : IRequest<ListObject<ManagerInfoObject>>
    {
        public int Id { get; set; }
        public PaginationObject Pagination { get; set; } = null!;
    }
}

using ECommerceManagement.API.DTOs;
using MediatR;

namespace ECommerceManagement.API.Queries.Shop
{
    public class GetShopsQuery : IRequest<ListObject<ShopInfoObject>>
    {
        public PaginationObject Pagination { get; set; } = null!;
    }
}

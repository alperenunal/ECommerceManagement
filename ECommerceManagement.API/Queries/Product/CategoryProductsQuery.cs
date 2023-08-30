using ECommerceManagement.API.DTOs;
using MediatR;

namespace ECommerceManagement.API.Queries.Product
{
    public class CategoryProductsQuery : IRequest<ListObject<ProductInfoObject>>
    {
        public Guid Id { get; set; }
        public PaginationObject Pagination { get; set; } = null!;
    }
}

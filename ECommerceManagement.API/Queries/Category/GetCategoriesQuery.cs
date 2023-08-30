using ECommerceManagement.API.DTOs;
using MediatR;

namespace ECommerceManagement.API.Queries.Category
{
    public class GetCategoriesQuery : IRequest<ListObject<CategoryInfoObject>>
    {
        public PaginationObject Pagination { get; set; } = null!;
    }
}

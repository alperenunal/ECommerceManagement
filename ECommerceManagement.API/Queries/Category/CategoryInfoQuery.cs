using ECommerceManagement.API.DTOs;
using MediatR;

namespace ECommerceManagement.API.Queries.Category
{
    public class CategoryInfoQuery : IRequest<CategoryInfoObject>
    {
        public Guid Id { get; set; }
    }
}

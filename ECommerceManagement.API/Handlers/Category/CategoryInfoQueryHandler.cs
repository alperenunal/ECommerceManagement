using ECommerceManagement.API.Data;
using ECommerceManagement.API.DTOs;
using ECommerceManagement.API.Exceptions;
using ECommerceManagement.API.Queries.Category;
using Microsoft.EntityFrameworkCore;

namespace ECommerceManagement.API.Handlers.Category
{
    public class CategoryInfoQueryHandler : BaseHandler<CategoryInfoQuery, CategoryInfoObject>
    {
        public CategoryInfoQueryHandler(EcommerceContext context) : base(context) { }

        public override async Task<CategoryInfoObject> Handle(CategoryInfoQuery request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException($"Category {request.Id} not found");
            return new CategoryInfoObject
            {
                Id = category.Id,
                Name = category.Name,
            };
        }
    }
}

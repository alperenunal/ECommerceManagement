using ECommerceManagement.API.Data;
using ECommerceManagement.API.DTOs;
using ECommerceManagement.API.Extensions;
using ECommerceManagement.API.Queries.Category;
using Microsoft.EntityFrameworkCore;

namespace ECommerceManagement.API.Handlers.Category
{
    public class GetCategoriesQueryHandler : BaseHandler<GetCategoriesQuery, ListObject<CategoryInfoObject>>
    {
        public GetCategoriesQueryHandler(EcommerceContext context) : base(context)
        {
            
        }

        public override async Task<ListObject<CategoryInfoObject>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var total = await _context.Categories.CountAsync(cancellationToken);
               
            var items = await _context.Categories
                .OrderBy(c => c.Name)
                .Paginate(request.Pagination.Offset, request.Pagination.Limit)
                .Select(c => new CategoryInfoObject
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync(cancellationToken);

            return new ListObject<CategoryInfoObject>
            {
                Offset = request.Pagination.Offset,
                Limit = request.Pagination.Limit,
                Total = total,
                Items = items,
            };
        }
    }
}

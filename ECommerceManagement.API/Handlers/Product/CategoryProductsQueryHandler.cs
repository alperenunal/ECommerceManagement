using ECommerceManagement.API.Data;
using ECommerceManagement.API.DTOs;
using ECommerceManagement.API.Extensions;
using ECommerceManagement.API.Queries.Product;
using Microsoft.EntityFrameworkCore;

namespace ECommerceManagement.API.Handlers.Product
{
    public class CategoryProductsQueryHandler : BaseHandler<CategoryProductsQuery, ListObject<ProductInfoObject>>
    {
        public CategoryProductsQueryHandler(EcommerceContext context) : base(context) { }

        public override async Task<ListObject<ProductInfoObject>> Handle(CategoryProductsQuery request, CancellationToken cancellationToken)
        {
            var products = _context.Products.AsNoTracking()
                .Include(p => p.Category)
                .Where(p => p.CategoryId == request.Id)
                .OrderBy(p => p.Name);

            var total = await products.CountAsync(cancellationToken);
                
            var items = await products.AsNoTracking()
                .Paginate(request.Pagination.Offset, request.Pagination.Limit)
                .Select(p => new ProductInfoObject
                {
                    Id = p.Id,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.Name,
                    Name = p.Name,
                    Price = p.Price,
                    AmountPerArea = p.AmountPerArea,
                })
                .ToListAsync(cancellationToken);

            return new ListObject<ProductInfoObject>
            {
                Offset = request.Pagination.Offset,
                Limit = request.Pagination.Limit,
                Total = total,
                Items = items,
            };
        }
    }
}

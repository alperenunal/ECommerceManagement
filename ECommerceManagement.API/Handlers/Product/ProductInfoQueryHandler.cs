using ECommerceManagement.API.Data;
using ECommerceManagement.API.DTOs;
using ECommerceManagement.API.Exceptions;
using ECommerceManagement.API.Queries.Product;
using Microsoft.EntityFrameworkCore;

namespace ECommerceManagement.API.Handlers.Product
{
    public class ProductInfoQueryHandler : BaseHandler<ProductInfoQuery, ProductInfoObject>
    {
        public ProductInfoQueryHandler(EcommerceContext context) : base(context) { }

        public override async Task<ProductInfoObject> Handle(ProductInfoQuery request, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .Include(product => product.Category)
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException($"Product {request.Id} not found");

            return new ProductInfoObject
            {
                Id = product.Id,
                CategoryId = product.CategoryId,
                CategoryName = product.Category.Name,
                Name = product.Name,
                Price = product.Price,
                AmountPerArea = product.AmountPerArea,
            };
        }
    }
}

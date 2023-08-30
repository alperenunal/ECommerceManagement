using ECommerceManagement.API.Commands.Product;
using ECommerceManagement.API.Data;

namespace ECommerceManagement.API.Handlers.Product
{
    public class CreateProductCommandHandler : BaseHandler<CreateProductCommand, Models.Product>
    {
        public CreateProductCommandHandler(EcommerceContext context) : base(context)
        {
        }

        public override async Task<Models.Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Models.Product
            {
                CategoryId = request.CategoryId,
                Name = request.Name,
                Price = request.Price,
            };

            await _context.Products.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            var stock = new Models.Stock
            {
                ProductId = product.Id,
                Amount = 0,
                StoreId = null,
            };
            await _context.Stocks.AddAsync(stock, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return product;
        }
    }
}

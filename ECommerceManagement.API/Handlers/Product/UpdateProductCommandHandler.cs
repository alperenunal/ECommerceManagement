using ECommerceManagement.API.Commands.Product;
using ECommerceManagement.API.Data;
using ECommerceManagement.API.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ECommerceManagement.API.Handlers.Product
{
    public class UpdateProductCommandHandler : BaseHandler<UpdateProductCommand>
    {
        public UpdateProductCommandHandler(EcommerceContext context) : base(context)
        {
        }

        public override async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Products.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException($"Category {request.Id} not found");

            category.Name = request.Name;
            category.Price = request.Price;
            _context.Products.Update(category);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

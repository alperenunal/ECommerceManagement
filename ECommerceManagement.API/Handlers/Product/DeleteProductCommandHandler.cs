using ECommerceManagement.API.Commands.Product;
using ECommerceManagement.API.Data;
using ECommerceManagement.API.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ECommerceManagement.API.Handlers.Product
{
    public class DeleteProductCommandHandler : BaseHandler<DeleteProductCommand>
    {
        public DeleteProductCommandHandler(EcommerceContext context) : base(context)
        {   
        }

        public override async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException($"Category {request.Id} not found");

            _context.Products.Remove(product);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

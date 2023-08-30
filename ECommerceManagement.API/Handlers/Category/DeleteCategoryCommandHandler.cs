using ECommerceManagement.API.Commands.Category;
using ECommerceManagement.API.Data;
using ECommerceManagement.API.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ECommerceManagement.API.Handlers.Category
{
    public class DeleteCategoryCommandHandler : BaseHandler<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandHandler(EcommerceContext context) : base(context)
        {
        }

        public override async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException($"Category {request.Id} not found");

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

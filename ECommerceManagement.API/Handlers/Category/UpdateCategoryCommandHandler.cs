using ECommerceManagement.API.Commands.Category;
using ECommerceManagement.API.Data;
using ECommerceManagement.API.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ECommerceManagement.API.Handlers.Category
{
    public class UpdateCategoryCommandHandler : BaseHandler<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandHandler(EcommerceContext context) : base(context)
        {
        }

        public override async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException($"Category {request.Id} not found");

            category.Name = request.Name;
            _context.Categories.Update(category);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

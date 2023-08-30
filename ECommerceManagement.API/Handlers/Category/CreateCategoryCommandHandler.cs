using ECommerceManagement.API.Commands.Category;
using ECommerceManagement.API.Data;

namespace ECommerceManagement.API.Handlers.Category
{
    public class CreateCategoryCommandHandler : BaseHandler<CreateCategoryCommand, Models.Category>
    {
        public CreateCategoryCommandHandler(EcommerceContext context) : base(context)
        {
        }

        public override async Task<Models.Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Models.Category
            {
                Name = request.Name,
            };

            await _context.Categories.AddAsync(category, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return category;
        }
    }
}

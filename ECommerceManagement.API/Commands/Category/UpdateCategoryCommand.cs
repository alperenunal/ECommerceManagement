using MediatR;

namespace ECommerceManagement.API.Commands.Category
{
    public class UpdateCategoryCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }
}

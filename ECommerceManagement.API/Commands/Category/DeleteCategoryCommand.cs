using MediatR;

namespace ECommerceManagement.API.Commands.Category
{
    public class DeleteCategoryCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}

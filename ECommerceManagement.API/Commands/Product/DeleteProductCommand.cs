using MediatR;

namespace ECommerceManagement.API.Commands.Product
{
    public class DeleteProductCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}

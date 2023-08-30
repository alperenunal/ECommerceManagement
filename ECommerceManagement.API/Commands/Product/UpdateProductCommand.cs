using MediatR;

namespace ECommerceManagement.API.Commands.Product
{
    public class UpdateProductCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public float Price { get; set; }
        //public float AmountPerArea { get; set; }
    }
}

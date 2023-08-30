using MediatR;

namespace ECommerceManagement.API.Commands.Product
{
    public class CreateProductCommand : IRequest<Models.Product>
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public float Price { get; set; }
        //public float AmountPerArea { get; set; }
    }
}

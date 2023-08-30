using ECommerceManagement.API.DTOs;
using MediatR;

namespace ECommerceManagement.API.Commands.Shop
{
    public class CreateShopCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public float Area { get; set; }
        public AddressObject Address { get; set; } = null!;
    }
}

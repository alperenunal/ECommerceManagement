using MediatR;

namespace ECommerceManagement.API.Commands.Manager
{
    public class CreateManagerCommand : IRequest
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string IdentityNumber { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int ShopId { get; set; }
    }
}

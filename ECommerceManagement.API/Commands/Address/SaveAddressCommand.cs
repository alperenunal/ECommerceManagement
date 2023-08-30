using MediatR;

namespace ECommerceManagement.API.Commands.Address
{
    public class SaveAddressCommand : IRequest
    {
        public Guid CustomerId { get; set; }
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
        public int Zipcode { get; set; }
    }
}

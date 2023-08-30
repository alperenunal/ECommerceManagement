using MediatR;

namespace ECommerceManagement.API.Commands.Order
{
    public class ProcessOrderCommand : IRequest
    {
        public Guid Id { get; set; }
        public int ShopId { get; set; }
        public bool Status { get; set; }
    }
}

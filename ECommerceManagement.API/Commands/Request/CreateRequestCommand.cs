using MediatR;

namespace ECommerceManagement.API.Commands.Request
{
    public class CreateRequestCommand : IRequest<Models.Request>
    {
        public int ShopId { get; set; }
        public Guid ManagerId { get; set; }
        public Guid ProductId { get; set; }
        public int Amount { get; set; }
    }
}

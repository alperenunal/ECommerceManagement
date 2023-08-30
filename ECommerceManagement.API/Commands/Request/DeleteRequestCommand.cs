using MediatR;

namespace ECommerceManagement.API.Commands.Request
{
    public class DeleteRequestCommand : IRequest
    {
        public Guid ManagerId { get; set; }
        public Guid RequestID { get; set; }
    }
}

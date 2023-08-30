using MediatR;

namespace ECommerceManagement.API.Commands.Request
{
    public class ProcessRequestCommand : IRequest
    {
        public Guid RequestId { get; set; }
        public bool Status { get; set; }
    }
}

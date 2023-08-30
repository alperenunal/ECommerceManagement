using ECommerceManagement.API.DTOs;
using MediatR;

namespace ECommerceManagement.API.Queries.Request
{
    public class RequestInfoQuery : IRequest<RequestInfoObject>
    {
        public Guid Id { get; set; }
    }
}

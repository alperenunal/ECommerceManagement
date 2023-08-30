using ECommerceManagement.API.DTOs;
using MediatR;

namespace ECommerceManagement.API.Queries.Manager
{
    public class ManagerInfoQuery : IRequest<ManagerInfoObject>
    {
        public Guid Id { get; set; }
    }
}

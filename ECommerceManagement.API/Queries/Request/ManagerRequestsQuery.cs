using ECommerceManagement.API.DTOs;
using MediatR;

namespace ECommerceManagement.API.Queries.Request
{
    public class ManagerRequestsQuery : IRequest<ListObject<RequestInfoObject>>
    {
        public Guid ManagerId { get; set; }
        public bool? Status { get; set; }
        public PaginationObject Pagination { get; set; } = null!;
    }
}

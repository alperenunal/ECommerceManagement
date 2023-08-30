using ECommerceManagement.API.DTOs;
using MediatR;

namespace ECommerceManagement.API.Queries.Request
{
    public class GetRequestsQuery : IRequest<ListObject<RequestInfoObject>>
    {
        public bool? Status { get; set; }
        public PaginationObject Pagination { get; set; } = null!;
    }
}

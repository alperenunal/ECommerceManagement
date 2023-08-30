using ECommerceManagement.API.DTOs;
using MediatR;

namespace ECommerceManagement.API.Queries.Warehouse
{
    public class WarehouseInfoQuery : IRequest<WarehouseInfoObject>
    {
        public PaginationObject Pagination { get; set; } = null!;
    }
}

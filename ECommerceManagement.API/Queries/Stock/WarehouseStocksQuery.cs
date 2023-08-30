using ECommerceManagement.API.DTOs;
using MediatR;

namespace ECommerceManagement.API.Queries.Stock
{
    public class WarehouseStocksQuery : IRequest<ListObject<StockInfoObject>>
    {
        public Guid CategoryId { get; set; }
        public PaginationObject Pagination { get; set; } = null!;
    }
}

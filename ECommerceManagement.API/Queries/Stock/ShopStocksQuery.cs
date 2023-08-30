using ECommerceManagement.API.DTOs;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ECommerceManagement.API.Queries.Stock
{
    public class ShopStocksQuery : IRequest<ListObject<StockInfoObject>>
    {
        public int ShopId { get; set; }
        public PaginationObject Pagination { get; set; } = null!;
    }
}

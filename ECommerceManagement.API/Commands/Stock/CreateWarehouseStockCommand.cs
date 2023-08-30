using MediatR;

namespace ECommerceManagement.API.Commands.Stock
{
    public class CreateWarehouseStockCommand : IRequest<Models.Stock>
    {
        public Guid ProductId { get; set; }
        public int Amount { get; set; }
    }
}

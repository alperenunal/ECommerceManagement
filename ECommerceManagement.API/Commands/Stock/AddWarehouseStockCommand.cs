using MediatR;

namespace ECommerceManagement.API.Commands.Stock
{
    public class AddWarehouseStockCommand : IRequest
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }
    }
}

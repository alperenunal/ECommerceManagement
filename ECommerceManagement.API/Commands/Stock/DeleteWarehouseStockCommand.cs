using MediatR;

namespace ECommerceManagement.API.Commands.Stock
{
    public class DeleteWarehouseStockCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}

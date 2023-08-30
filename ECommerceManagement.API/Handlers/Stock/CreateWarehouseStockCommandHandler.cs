using ECommerceManagement.API.Commands.Stock;
using ECommerceManagement.API.Data;

namespace ECommerceManagement.API.Handlers.Stock
{
    public class CreateWarehouseStockCommandHandler : BaseHandler<CreateWarehouseStockCommand, Models.Stock>
    {
        public CreateWarehouseStockCommandHandler(EcommerceContext context) : base(context)
        {
        }

        public override async Task<Models.Stock> Handle(CreateWarehouseStockCommand request, CancellationToken cancellationToken)
        {
            var stock = new Models.Stock
            {
                StoreId = null,
                ProductId = request.ProductId,
                Amount = request.Amount,
            };

            await _context.Stocks.AddAsync(stock, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return stock;
        }
    }
}

using ECommerceManagement.API.Commands.Stock;
using ECommerceManagement.API.Data;
using ECommerceManagement.API.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ECommerceManagement.API.Handlers.Stock
{
    public class AddWarehouseStockCommandHandler : BaseHandler<AddWarehouseStockCommand>
    {
        public AddWarehouseStockCommandHandler(EcommerceContext context) : base(context)
        {
        }

        public override async Task Handle(AddWarehouseStockCommand request, CancellationToken cancellationToken)
        {
            var stock = await _context.Stocks.AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException("Stock not found");

            stock.Amount += request.Amount;
            _context.Update(stock);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

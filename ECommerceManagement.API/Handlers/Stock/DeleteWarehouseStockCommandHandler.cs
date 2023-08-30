using ECommerceManagement.API.Commands.Stock;
using ECommerceManagement.API.Data;
using ECommerceManagement.API.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ECommerceManagement.API.Handlers.Stock
{
    public class DeleteWarehouseStockCommandHandler : BaseHandler<DeleteWarehouseStockCommand>
    {
        public DeleteWarehouseStockCommandHandler(EcommerceContext context) : base(context)
        {
        }

        public override async Task Handle(DeleteWarehouseStockCommand request, CancellationToken cancellationToken)
        {
            var stock = await _context.Stocks.AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException("Stock not found");

            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

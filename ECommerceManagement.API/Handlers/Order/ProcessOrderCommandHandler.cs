using ECommerceManagement.API.Commands.Order;
using ECommerceManagement.API.Data;
using ECommerceManagement.API.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ECommerceManagement.API.Handlers.Order
{
    public class ProcessOrderCommandHandler : BaseHandler<ProcessOrderCommand>
    {
        public ProcessOrderCommandHandler(EcommerceContext context) : base(context)
        {
        }

        public override async Task Handle(ProcessOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.AsNoTracking()
                .Include(o => o.OrderProducts)
                .FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException("Order not found");

            if (order.ShopId != request.ShopId)
            {
                throw new UnauthorizedException("Can't access other shops orders");
            }

            var orders = order.OrderProducts.ToList();

            orders.ForEach(o =>
            {
                var stock = _context.Stocks.AsNoTracking()
                    .Where(s => s.StoreId == request.ShopId)
                    .Where(s => s.ProductId == o.ProductId).First();

                if (stock.Amount < o.Amount)
                {
                    throw new BadRequestException("Don't have enough products at stock");
                }

                stock.Amount -= o.Amount;
                _context.Stocks.Update(stock);
            });

            order.Status = request.Status;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

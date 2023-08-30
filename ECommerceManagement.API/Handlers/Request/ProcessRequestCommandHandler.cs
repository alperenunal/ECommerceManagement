using ECommerceManagement.API.Commands.Request;
using ECommerceManagement.API.Data;
using ECommerceManagement.API.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ECommerceManagement.API.Handlers.Request
{
    public class ProcessRequestCommandHandler : BaseHandler<ProcessRequestCommand>
    {
        public ProcessRequestCommandHandler(EcommerceContext context) : base(context) { }

        public override async Task Handle(ProcessRequestCommand request, CancellationToken cancellationToken)
        {
            var req = await _context.Requests.AsNoTracking()
                .FirstOrDefaultAsync(req => req.Id == request.RequestId, cancellationToken)
                ?? throw new NotFoundException($"Request {request.RequestId} not found");

            if (request.Status == true)
            {
                var wh_stock = await _context.Stocks.AsNoTracking()
                    .Where(s => s.StoreId == null)
                    .Where(s => s.ProductId == req.ProductId)
                    .FirstAsync(cancellationToken);

                if (wh_stock.Amount < req.Amount)
                {
                    throw new Exception(); // FIXME
                }

                var shop_stock = await _context.Stocks.AsNoTracking()
                    .Where(s => s.StoreId == req.ShopId)
                    .Where(s => s.ProductId == req.ProductId)
                    .FirstOrDefaultAsync(cancellationToken);

                if (shop_stock == null)
                {
                    var stock = new Models.Stock
                    {
                        ProductId = req.ProductId,
                        StoreId = req.ShopId,
                        Amount = req.Amount,
                    };
                    await _context.Stocks.AddAsync(stock, cancellationToken);
                } else
                {
                    shop_stock.Amount += req.Amount;
                    _context.Stocks.Update(wh_stock);
                }

                wh_stock.Amount -= req.Amount;
                _context.Stocks.Update(wh_stock);
            }

            req.Status = request.Status;
            _context.Requests.Update(req);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

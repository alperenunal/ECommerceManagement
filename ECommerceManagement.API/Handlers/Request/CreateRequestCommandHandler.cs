using ECommerceManagement.API.Commands.Request;
using ECommerceManagement.API.Data;

namespace ECommerceManagement.API.Handlers.Request
{
    public class CreateRequestCommandHandler : BaseHandler<CreateRequestCommand, Models.Request>
    {
        public CreateRequestCommandHandler(EcommerceContext context) : base(context) { }

        public override async Task<Models.Request> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
        {
            var req = new Models.Request
            {
                ManagerId = request.ManagerId,
                ShopId = request.ShopId,
                ProductId = request.ProductId,
                Amount = request.Amount,
                Status = null
            };

            try
            {
                await _context.Requests.AddAsync(req, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return req;
            } catch (Exception ex)
            {
                // Log, throw exception, etc.
                throw new Exception(ex.Message);
            }
        }
    }
}

using ECommerceManagement.API.Commands.Request;
using ECommerceManagement.API.Data;
using ECommerceManagement.API.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ECommerceManagement.API.Handlers.Request
{
    public class DeleteRequestCommandHandler : BaseHandler<DeleteRequestCommand>
    {
        public DeleteRequestCommandHandler(EcommerceContext context) : base(context) {}

        public override async Task Handle(DeleteRequestCommand request, CancellationToken cancellationToken)
        {
            var req = await _context.Requests.AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == request.RequestID, cancellationToken)
                ?? throw new NotFoundException($"Request {request.RequestID} not found");

            if (req.ManagerId != request.ManagerId)
            {
                throw new UnauthorizedException("Can't delete other managers requests");
            }

            _context.Requests.Remove(req);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

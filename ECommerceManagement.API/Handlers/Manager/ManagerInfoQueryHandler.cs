using ECommerceManagement.API.Data;
using ECommerceManagement.API.DTOs;
using ECommerceManagement.API.Exceptions;
using ECommerceManagement.API.Queries.Manager;
using Microsoft.EntityFrameworkCore;

namespace ECommerceManagement.API.Handlers.Manager
{
    public class ManagerInfoQueryHandler : BaseHandler<ManagerInfoQuery, ManagerInfoObject>
    {
        public ManagerInfoQueryHandler(EcommerceContext context) : base(context) { }

        public override async Task<ManagerInfoObject> Handle(ManagerInfoQuery request, CancellationToken cancellationToken)
        {
            var manager = await _context.Managers
                .Include(m => m.IdNavigation)
                //.Include(m => m.Shop)
                .Include(m => m.Shop.IdNavigation)
                .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException("Manager not found");

            return new ManagerInfoObject
            {
                Id = manager.Id,
                Name = manager.IdNavigation.Name,
                Surname = manager.IdNavigation.Surname,
                Email = manager.IdNavigation.Email,
                PhoneNumber = manager.IdNavigation.PhoneNumber,
                ShopId = manager.ShopId,
                ShopName = manager.Shop.IdNavigation.Name,
            };
        }
    }
}

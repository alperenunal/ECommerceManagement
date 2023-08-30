using ECommerceManagement.API.Data;
using ECommerceManagement.API.DTOs;
using ECommerceManagement.API.Extensions;
using ECommerceManagement.API.Queries.Manager;
using Microsoft.EntityFrameworkCore;

namespace ECommerceManagement.API.Handlers.Manager
{
    public class ShopManagersQueryHandler : BaseHandler<ShopManagersQuery, ListObject<ManagerInfoObject>>
    {
        public ShopManagersQueryHandler(EcommerceContext context) : base(context) { }

        public override async Task<ListObject<ManagerInfoObject>> Handle(ShopManagersQuery request, CancellationToken cancellationToken)
        {
            var managers = _context.Managers.AsNoTracking()
                .Where(manager => manager.ShopId == request.Id)
                .Include(manager => manager.IdNavigation)
                .Include(manager => manager.Shop)
                .OrderBy(manager => manager.IdNavigation.Surname);

            var total = await managers.CountAsync(cancellationToken);

            var items = await managers.AsNoTracking()
                .Paginate(request.Pagination.Offset, request.Pagination.Limit)
                .Select(manager => new ManagerInfoObject
                {
                    Id = manager.Id,
                    Name = manager.IdNavigation.Name,
                    Surname = manager.IdNavigation.Surname,
                    Email = manager.IdNavigation.Email,
                    PhoneNumber = manager.IdNavigation.PhoneNumber,
                    ShopId = manager.ShopId,
                    ShopName = manager.Shop.IdNavigation.Name,
                })
                .ToListAsync(cancellationToken);

            return new ListObject<ManagerInfoObject>
            {
                Offset = request.Pagination.Offset,
                Limit = request.Pagination.Limit,
                Total = total,
                Items = items,
            };
        }
    }
}

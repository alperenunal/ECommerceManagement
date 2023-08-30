using ECommerceManagement.API.Data;
using ECommerceManagement.API.DTOs;
using ECommerceManagement.API.Extensions;
using ECommerceManagement.API.Queries.Order;
using Microsoft.EntityFrameworkCore;

namespace ECommerceManagement.API.Handlers.Order
{
    public class CustomerOrdersQueryHandler : BaseHandler<CustomerOrdersQuery, ListObject<OrderInfoObject>>
    {
        public CustomerOrdersQueryHandler(EcommerceContext context) : base(context)
        {
        }

        public override async Task<ListObject<OrderInfoObject>> Handle(CustomerOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = _context.Orders.AsNoTracking()
                .Include(o => o.Address)
                .Include(o => o.OrderProducts)
                .Where(o => o.CustomerId == request.Id);
            var total = await orders.CountAsync(cancellationToken);

            var items = await orders
                .Paginate(request.Pagination.Offset, request.Pagination.Limit)
                .Select(o => new OrderInfoObject
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    Price = o.Price,
                    Status = o.Status,
                    Products = o.OrderProducts.Select(p => new OrderProductInfoObject
                    {
                        ProductId = p.ProductId,
                        ProductName = p.Product.Name,
                        Price = p.Product.Price,
                        Amount = p.Amount,
                    }).ToList(),
                    Address = new AddressInfoObject
                    {
                        Id = o.AddressId,
                        Address = o.Address.Address1,
                        Zipcode = o.Address.Zipcode,
                        City = o.Address.City,
                        Country = o.Address.Country,
                    },
                })
                .ToListAsync(cancellationToken);

            return new ListObject<OrderInfoObject>
            {
                Offset = request.Pagination.Offset,
                Limit = request.Pagination.Limit,
                Total = total,
                Items = items,
            };
        }
    }
}

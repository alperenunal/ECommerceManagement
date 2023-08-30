using ECommerceManagement.API.Data;
using ECommerceManagement.API.DTOs;
using ECommerceManagement.API.Queries.Card;
using Microsoft.EntityFrameworkCore;

namespace ECommerceManagement.API.Handlers.Card
{
    public class CustomerCardsQueryHandler : BaseHandler<CustomerCardsQuery, ListObject<CardInfoObject>>
    {
        public CustomerCardsQueryHandler(EcommerceContext context) : base(context)
        {
        }

        public override async Task<ListObject<CardInfoObject>> Handle(CustomerCardsQuery request, CancellationToken cancellationToken)
        {
            var count = await _context.Cards.AsNoTracking()
                .CountAsync(c => c.CustomerId == request.Id, cancellationToken);

            var items = await _context.Cards.AsNoTracking()
                .Where(c => c.CustomerId == request.Id)
                .Select(c => new CardInfoObject
                {
                    Id = c.Id,
                    LastFourDigit = c.CardNumber.Substring(12, 4),
                    CardName = c.CardName,
                })
                .ToListAsync(cancellationToken);

            return new ListObject<CardInfoObject>
            {
                Offset = request.Pagination.Offset,
                Limit = request.Pagination.Limit,
                Total = count,
                Items = items,
            };
        }
    }
}

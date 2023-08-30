using ECommerceManagement.API.DTOs;
using MediatR;

namespace ECommerceManagement.API.Queries.Card
{
    public class CustomerCardsQuery : IRequest<ListObject<CardInfoObject>>
    {
        public Guid Id { get; set; }
        public PaginationObject Pagination { get; set; } = null!;
    }
}

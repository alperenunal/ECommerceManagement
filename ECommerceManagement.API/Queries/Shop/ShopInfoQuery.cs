using ECommerceManagement.API.DTOs;
using MediatR;

namespace ECommerceManagement.API.Queries.Shop
{
    public class ShopInfoQuery : IRequest<ShopInfoObject>
    {
        public int Id { get; set; }
    }
}

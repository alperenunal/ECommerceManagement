using ECommerceManagement.API.DTOs;
using MediatR;

namespace ECommerceManagement.API.Queries.Product
{
    public class ProductInfoQuery : IRequest<ProductInfoObject>
    {
        public Guid Id { get; set; }
    }
}

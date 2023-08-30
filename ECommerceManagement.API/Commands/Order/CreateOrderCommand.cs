using ECommerceManagement.API.DTOs;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ECommerceManagement.API.Commands.Order
{
    public class CreateOrderCommand : IRequest
    {
        [Required]
        public Guid CustomerId { get; set; }
        [Required]
        public List<OrderProductObject> Products { get; set; } = null!;
        [Required]
        public Guid AddressId { get; set; }
        [Required]
        public Guid CardId { get; set; }
    }
}

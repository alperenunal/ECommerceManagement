using System.ComponentModel.DataAnnotations;

namespace ECommerceManagement.API.DTOs
{
    public class OrderProductObject
    {
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}

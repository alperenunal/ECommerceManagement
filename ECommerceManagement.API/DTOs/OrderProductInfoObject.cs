using System.ComponentModel.DataAnnotations;

namespace ECommerceManagement.API.DTOs
{
    public class OrderProductInfoObject
    {
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public string ProductName { get; set; } = null!;
        [Required]
        public int Amount { get; set; }
        [Required]
        public float Price { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace ECommerceManagement.API.DTOs
{
    public class OrderInfoObject
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public List<OrderProductInfoObject> Products { get; set; } = null!;
        [Required]
        public AddressInfoObject Address { get; set; } = null!;
    }
}

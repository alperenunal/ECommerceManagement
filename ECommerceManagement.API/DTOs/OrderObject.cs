using System.ComponentModel.DataAnnotations;

namespace ECommerceManagement.API.DTOs
{
    public class OrderObject
    {
        [Required]
        public List<OrderProductObject> Products { get; set; } = null!;
        [Required]
        public Guid AddressId { get; set; }
        [Required]
        public Guid CardId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace ECommerceManagement.API.DTOs
{
    public class ShopObject
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public float Area { get; set; }
        [Required]
        public AddressObject Address { get; set; } = null!;
    }
}

using System.ComponentModel.DataAnnotations;

namespace ECommerceManagement.API.DTOs
{
    public class ProductInfoObject
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; } = null!;
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public float Price { get; set; }
        [Required]
        public float AmountPerArea { get; set; }
    }
}

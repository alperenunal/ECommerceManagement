using System.ComponentModel.DataAnnotations;

namespace ECommerceManagement.API.DTOs
{
    public class ProductObject
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public float Price { get; set; }
        //[Required]
        //public float AmountPerArea { get; set; }
    }
}

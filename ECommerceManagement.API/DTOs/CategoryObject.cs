using System.ComponentModel.DataAnnotations;

namespace ECommerceManagement.API.DTOs
{
    public class CategoryObject
    {
        [Required]
        public string Name { get; set; } = null!;
    }
}

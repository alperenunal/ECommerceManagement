using System.ComponentModel.DataAnnotations;

namespace ECommerceManagement.API.DTOs
{
    public class CategoryInfoObject
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;

    }
}

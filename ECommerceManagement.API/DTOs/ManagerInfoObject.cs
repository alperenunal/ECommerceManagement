using System.ComponentModel.DataAnnotations;

namespace ECommerceManagement.API.DTOs
{
    public class ManagerInfoObject
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Surname { get; set; } = null!;
        [Required]
        public int ShopId { get; set; }
        [Required]
        public string ShopName { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string PhoneNumber { get; set; } = null!;
    }
}

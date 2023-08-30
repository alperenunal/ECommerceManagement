using System.ComponentModel.DataAnnotations;

namespace ECommerceManagement.API.DTOs
{
    public class UserInfoObject
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Role { get; set; } = null!;
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Surname { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string PhoneNumber { get; set; } = null!;
        [Required]
        public string IdentityNumber { get; set; } = null!;
    }
}

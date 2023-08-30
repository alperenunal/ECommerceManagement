using System.ComponentModel.DataAnnotations;

namespace ECommerceManagement.API.DTOs
{
    public class LoginCredentials
    {
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string Role { get; set; } = null!;
    }
}

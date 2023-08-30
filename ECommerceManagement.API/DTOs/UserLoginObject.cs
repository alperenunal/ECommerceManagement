using System.ComponentModel.DataAnnotations;

namespace ECommerceManagement.API.DTOs
{
    public class UserLoginObject
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Token { get; set; } = null!;
    }
}

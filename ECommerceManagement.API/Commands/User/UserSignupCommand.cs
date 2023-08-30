using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ECommerceManagement.API.Commands.User
{
    public class UserSignupCommand : IRequest<Models.User>
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Surname { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string IdentityNumber { get; set; } = null!;
        [Required]
        public string PhoneNumber { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}

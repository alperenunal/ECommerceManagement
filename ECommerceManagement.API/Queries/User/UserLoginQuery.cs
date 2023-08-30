using ECommerceManagement.API.DTOs;
using MediatR;

namespace ECommerceManagement.API.Queries.User
{
    public class UserLoginQuery : IRequest<UserLoginObject>
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}

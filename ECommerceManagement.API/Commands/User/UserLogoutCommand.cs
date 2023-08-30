using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceManagement.API.Commands.User
{
    public class UserLogoutCommand : IRequest
    {
        public string Token { get; set; } = null!;
    }
}

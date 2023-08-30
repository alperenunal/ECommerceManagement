using ECommerceManagement.API.Commands.User;
using ECommerceManagement.API.DTOs;
using ECommerceManagement.API.Extensions;
using ECommerceManagement.API.Queries.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ECommerceManagement.API.Controllers
{
    public class UserController : BaseController
    {
        public UserController(IMediator mediator) : base(mediator) { }

        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UserLoginObject), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login([FromBody, Required] LoginCredentials credentials)
        {
            var resp = await _mediator.Send(new UserLoginQuery
            {
                Email = credentials.Email,
                Password = credentials.Password,
                Role = credentials.Role,
            });
            return Ok(resp);
        }

        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Logout([FromHeader(Name = "Authorization"), Required] string auth)
        {
            var token = auth.Split(" ")[1]!;
            await _mediator.Send(new UserLogoutCommand
            {
                Token = token,
            });
            return NoContent();
        }

        [HttpGet("me")]
        [ProducesResponseType(typeof(UserInfoObject), StatusCodes.Status200OK)]
        public async Task<IActionResult> Info()
        {
            var id = User.GetSub();
            var res = await _mediator.Send(new UserInfoQuery
            {
                Id = Guid.Parse(id)
            });
            return Ok(res);
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Signup([FromBody, Required] UserSignupObject user)
        {
            var newUser = await _mediator.Send(new UserSignupCommand
            {
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                IdentityNumber = user.IdentityNumber,
                PhoneNumber = user.PhoneNumber,
                Password = user.Password,
            });

            return Created("/api/Users/me", newUser);
        }
    }
}

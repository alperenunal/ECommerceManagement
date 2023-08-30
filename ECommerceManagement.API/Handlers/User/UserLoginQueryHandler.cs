using ECommerceManagement.API.Data;
using ECommerceManagement.API.Exceptions;
using ECommerceManagement.API.Queries.User;
using ECommerceManagement.API.DTOs;
using ECommerceManagement.API.Services;
using Microsoft.EntityFrameworkCore;

namespace ECommerceManagement.API.Handlers.User
{
    public class UserLoginQueryHandler : BaseHandler<UserLoginQuery, UserLoginObject>
    {
        private readonly TokenService _tokenService;

        public UserLoginQueryHandler(EcommerceContext context, TokenService tokenService) : base(context)
        {
            _tokenService = tokenService;
        }

        public async override Task<UserLoginObject> Handle(UserLoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(user => user.RoleNavigation)
                .FirstOrDefaultAsync(user => user.Email == request.Email, cancellationToken) 
                ?? throw new NotFoundException("User not found");

            var hash = PasswordService.GetHash(request.Password, user.Salt);
            if (hash != user.Hash)
            {
                throw new NotFoundException("Wrong email or password");
            }

            if (request.Role != user.RoleNavigation.Name)
            {
                throw new NotFoundException("Wrong role");
            }

            var claims = new Dictionary<string, string>
            {
                { "sub", user.Id.ToString() },
                { "name", user.Name },
                { "surname", user.Surname },
                { "role", user.RoleNavigation.Name },
            };

            switch (user.RoleNavigation.Name)
            {
                case "Admin":
                    break;
                case "Manager":
                    var manager = await _context.Managers.AsNoTracking()
                        .FirstAsync(m => m.Id == user.Id, cancellationToken);
                    claims.Add("shop_id", manager.ShopId.ToString());
                    break;
                case "Customer":
                    break;
            }

            return new UserLoginObject
            {
                Id = user.Id,
                Token = _tokenService.GetToken(claims)
            };
        }
    }
}

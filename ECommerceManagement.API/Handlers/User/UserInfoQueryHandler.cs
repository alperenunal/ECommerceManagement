using ECommerceManagement.API.Data;
using ECommerceManagement.API.DTOs;
using ECommerceManagement.API.Exceptions;
using ECommerceManagement.API.Queries.User;
using Microsoft.EntityFrameworkCore;

namespace ECommerceManagement.API.Handlers.User
{
    public class UserInfoQueryHandler : BaseHandler<UserInfoQuery, UserInfoObject>
    {
        public UserInfoQueryHandler(EcommerceContext context) : base(context) { }

        public override async Task<UserInfoObject> Handle(UserInfoQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(u => u.RoleNavigation)
                .FirstOrDefaultAsync(user => user.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException("User not found");

            return new UserInfoObject
            {
                Id = user.Id,
                Role = user.RoleNavigation.Name,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                IdentityNumber = user.IdentityNumber,
                PhoneNumber = user.PhoneNumber,
            };
        }
    }
}

using ECommerceManagement.API.Commands.User;
using ECommerceManagement.API.Data;
using ECommerceManagement.API.Exceptions;
using ECommerceManagement.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ECommerceManagement.API.Handlers.User
{
    public class UserSignupCommandHandler : BaseHandler<UserSignupCommand, Models.User>
    {
        public UserSignupCommandHandler(EcommerceContext context) : base(context)
        {
        }

        public override async Task<Models.User> Handle(UserSignupCommand request, CancellationToken cancellationToken)
        {
            var salt = PasswordService.GetSalt();
            var hash = PasswordService.GetHash(request.Password, salt);
            var role = await _context.Roles.FirstAsync(r => r.Name == "Customer", cancellationToken);

            var user = new Models.User
            {
                Role = role.Id,
                Name = request.Name,
                Surname = request.Surname,
                Email = request.Email,
                IdentityNumber = request.IdentityNumber,
                PhoneNumber = request.PhoneNumber,
                Salt = salt,
                Hash = hash,
            };

            try
            {
                await _context.Users.AddAsync(user, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                await _context.Customers.AddAsync(new Models.Customer
                {
                    Id = user.Id,
                }, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }

            return user;
        }
    }
}

using ECommerceManagement.API.Commands.Manager;
using ECommerceManagement.API.Data;
using ECommerceManagement.API.Services;
using Microsoft.EntityFrameworkCore;

namespace ECommerceManagement.API.Handlers.Manager
{
    public class CreateManagerCommandHandler : BaseHandler<CreateManagerCommand>
    {
        public CreateManagerCommandHandler(EcommerceContext context) : base(context)
        {
        }

        public override async Task Handle(CreateManagerCommand request, CancellationToken cancellationToken)
        {
            var managerRole = await _context.Roles
                .FirstAsync(r => r.Name == "Manager", cancellationToken);
            var password = $"manager_{request.Name}_{request.IdentityNumber[7..]}";
            var salt = PasswordService.GetSalt();
            var hash = PasswordService.GetHash(password, salt);

            var user = new Models.User
            {
                Name = request.Name,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                IdentityNumber = request.IdentityNumber,
                Surname = request.Surname,
                Role = managerRole.Id,
                Salt = salt,
                Hash = hash,
            };

            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var manager = new Models.Manager
            {
                Id = user.Id,
                ShopId = request.ShopId,
            };

            await _context.Managers.AddAsync(manager, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

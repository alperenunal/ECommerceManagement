using ECommerceManagement.API.Commands.User;
using ECommerceManagement.API.Data;
using Microsoft.Extensions.Caching.Memory;

namespace ECommerceManagement.API.Handlers.User
{
    public class UserLogoutCommandHandler : BaseHandler<UserLogoutCommand>
    {
        private readonly IMemoryCache _cache;

        public UserLogoutCommandHandler(EcommerceContext context, IMemoryCache cache) : base(context)
        {
            _cache = cache;
        }

        public override Task Handle(UserLogoutCommand request, CancellationToken cancellationToken)
        {
            _cache.Set(request.Token, true);
            return Task.CompletedTask;
        }
    }
}

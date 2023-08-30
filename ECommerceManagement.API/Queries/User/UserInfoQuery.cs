using ECommerceManagement.API.DTOs;
using MediatR;

namespace ECommerceManagement.API.Queries.User
{
    public class UserInfoQuery : IRequest<UserInfoObject>
    {
        public Guid Id { get; set; }
    }
}

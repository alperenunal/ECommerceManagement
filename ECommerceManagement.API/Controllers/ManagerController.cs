using ECommerceManagement.API.Commands.Order;
using ECommerceManagement.API.DTOs;
using ECommerceManagement.API.Exceptions;
using ECommerceManagement.API.Extensions;
using ECommerceManagement.API.Queries.Manager;
using ECommerceManagement.API.Queries.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ECommerceManagement.API.Controllers
{
    public class ManagerController : BaseController
    {
        public ManagerController(IMediator mediator) : base(mediator) { }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Manager")]
        [ProducesResponseType(typeof(ManagerInfoObject), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetInfo([FromRoute] Guid id)
        {
            var role = User.GetRole();
            var userId = User.GetSub();

            if (role == "Manager" && Guid.Parse(userId) != id)
            {
                return Unauthorized(new ErrorObject
                {
                    Status = 401,
                    Message = "Invalid token"
                });
            }

            var res = await _mediator.Send(new ManagerInfoQuery
            {
                Id = id
            });
            return Ok(res);
        }

        [HttpGet("{id}/requests")]
        [Authorize(Roles = "Admin, Manager")]
        [ProducesResponseType(typeof(ListObject<RequestInfoObject>), 200)]
        public async Task<IActionResult> GetRequests([FromRoute] Guid id, [FromQuery] bool? status,
                                                     [FromQuery] PaginationObject p)
        {
            var role = User.GetRole();
            var userId = User.GetSub();

            if (role == "Manager" && Guid.Parse(userId) != id)
            {
                return Unauthorized(new ErrorObject
                {
                    Status = 401,
                    Message = "Invalid token"
                });
            }

            var res = await _mediator.Send(new ManagerRequestsQuery
            {
                ManagerId = id,
                Pagination = p,
                Status = status
            });
            return Ok(res);
        }

        [HttpPut("{id}/orders/{orderId}")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ProcessOrder([FromRoute] Guid id, [FromRoute] Guid orderId, [FromBody, Required] bool status)
        {
            var managerId = User.GetSub();
            if (id.ToString() != managerId)
            {
                throw new UnauthorizedException("Wrong manager id");
            }

            var shopId = User.GetShopId();
            await _mediator.Send(new ProcessOrderCommand
            {
                Id = orderId,
                ShopId = int.Parse(shopId),
                Status = status,
            });

            return NoContent();
        }
    }
}

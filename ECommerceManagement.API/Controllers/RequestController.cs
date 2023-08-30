using ECommerceManagement.API.Commands.Request;
using ECommerceManagement.API.DTOs;
using ECommerceManagement.API.Extensions;
using ECommerceManagement.API.Queries.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ECommerceManagement.API.Controllers
{
    public class RequestController : BaseController
    {
        public RequestController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ListObject<RequestInfoObject>), 200)]
        public async Task<IActionResult> GetAllRequests([FromQuery] bool? status, [FromQuery] PaginationObject page)
        {
            return Ok(await _mediator.Send(new GetRequestsQuery
            {
                Status = status,
                Pagination = page,
            }));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Manager")]
        [ProducesResponseType(typeof(RequestInfoObject), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Info([FromRoute] Guid id)
        {
            var role = User.GetRole();
            var userId = User.GetSub();

            if (role == "Manager" && Guid.Parse(userId) != id)
            {
                return Unauthorized(new ErrorObject
                {
                    Status = 401,
                    Message = "Can't access other managers requests"
                });
            }

            return Ok(await _mediator.Send(new RequestInfoQuery { Id = id }));
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody, Required] RequestObject request)
        {
            var managerId = User.GetSub();
            var shopId = User.GetShopId();

            var res = await _mediator.Send(new CreateRequestCommand
            {
                ManagerId = Guid.Parse(managerId),
                ShopId = int.Parse(shopId),
                ProductId = request.ProductId,
                Amount = request.Amount,
            });

            return Created($"/api/Request/{res.Id}", res);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Process([FromRoute] Guid id, [FromQuery, Required] bool status)
        {
            await _mediator.Send(new ProcessRequestCommand
            {
                RequestId = id,
                Status = status
            });
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Manager")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _mediator.Send(new DeleteRequestCommand
            {
                ManagerId = Guid.Parse(User.GetSub()),
                RequestID = id
            });
            return NoContent();
        }
    }
}

using ECommerceManagement.API.Commands.Manager;
using ECommerceManagement.API.Commands.Shop;
using ECommerceManagement.API.DTOs;
using ECommerceManagement.API.Queries.Manager;
using ECommerceManagement.API.Queries.Order;
using ECommerceManagement.API.Queries.Request;
using ECommerceManagement.API.Queries.Shop;
using ECommerceManagement.API.Queries.Stock;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ECommerceManagement.API.Controllers
{
    public class ShopController : BaseController
    {
        public ShopController(IMediator mediator) : base(mediator) { }

        [HttpPost("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> CreateShop([FromRoute] int id, [FromBody, Required] ShopObject shop)
        {
            await _mediator.Send(new CreateShopCommand
            {
                Id = id,
                Name = shop.Name,
                Address = shop.Address,
                Area = shop.Area,
            });
            return NoContent();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ShopInfoObject), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Info([FromRoute] int id)
        {
            var resp = await _mediator.Send(new ShopInfoQuery { Id = id });
            return Ok(resp);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ListObject<ShopInfoObject>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetShops([FromQuery] PaginationObject page)
        {
            return Ok(await _mediator.Send(new GetShopsQuery
            {
                Pagination = page,
            }));
        }

        [HttpGet("{id}/managers")]
        [Authorize(Roles = "Admin, Manager")]
        [ProducesResponseType(typeof(ListObject<ManagerInfoObject>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Managers([FromRoute] int id, [FromQuery] PaginationObject page)
        {
            return Ok(await _mediator.Send(new ShopManagersQuery
            {
                Id = id,
                Pagination = page,
            }));
        }

        [HttpPost("{id}/managers")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> CreateManager([FromRoute] int id, [FromBody, Required] ManagerObject manager)
        {
            await _mediator.Send(new CreateManagerCommand
            {
                ShopId = id,
                Name = manager.Name,
                Surname = manager.Surname,
                Email = manager.Email,
                IdentityNumber = manager.IdentityNumber,
                PhoneNumber = manager.PhoneNumber,
            });
            return NoContent();
        }

        [HttpGet("{id}/requests")]
        [Authorize(Roles = "Admin, Manager")]
        [ProducesResponseType(typeof(ListObject<RequestInfoObject>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Requests([FromRoute] int id, [FromQuery] bool? status, [FromQuery] PaginationObject page)
        {
            var res = await _mediator.Send(new ShopRequestsQuery
            {
                Id = id,
                Status = status,
                Pagination = page
            });
            return Ok(res);
        }

        [HttpGet("{id}/orders")]
        [Authorize(Roles = "Admin, Manager")]
        [ProducesResponseType(typeof(ListObject<OrderInfoObject>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Orders([FromRoute] int id, [FromQuery] PaginationObject page)
        {
            return Ok(await _mediator.Send(new ShopOrdersQuery
            {
                Id = id,
                Pagination = page,
            }));
        }

        [HttpGet("{id}/stocks")]
        [Authorize(Roles = "Admin, Manager")]
        [ProducesResponseType(typeof(ListObject<StockInfoObject>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Stocks([FromRoute] int id, [FromQuery] PaginationObject page)
        {
            return Ok(await _mediator.Send(new ShopStocksQuery
            {
                ShopId = id,
                Pagination = page,
            }));
        }
    }
}

using ECommerceManagement.API.Commands.Address;
using ECommerceManagement.API.Commands.Card;
using ECommerceManagement.API.Commands.Order;
using ECommerceManagement.API.DTOs;
using ECommerceManagement.API.Models;
using ECommerceManagement.API.Queries.Address;
using ECommerceManagement.API.Queries.Card;
using ECommerceManagement.API.Queries.Order;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ECommerceManagement.API.Controllers
{
    public class CustomerController : BaseController
    {
        public CustomerController(IMediator mediator) : base(mediator) { }

        [HttpGet("{id}")]
        [Authorize(Roles = "Customer")]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Info([FromRoute] Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}/addresses")]
        [Authorize(Roles = "Customer")]
        [ProducesResponseType(typeof(ListObject<AddressInfoObject>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAddresses([FromRoute] Guid id, [FromQuery] PaginationObject page)
        {
            return Ok(await _mediator.Send(new CustomerAddressesQuery
            {
                Id = id,
                Pagination = page
            }));
        }

        [HttpPost("{id}/addresses")]
        [Authorize(Roles = "Customer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> SaveAddress([FromRoute] Guid id, [FromBody, Required] AddressObject address)
        {
            await _mediator.Send(new SaveAddressCommand
            {
                CustomerId = id,
                Address = address.Address,
                Zipcode = address.Zipcode,
                City = address.City,
                Country = address.Country,
            });
            return NoContent();
        }

        [HttpGet("{id}/orders")]
        [Authorize(Roles = "Customer")]
        [ProducesResponseType(typeof(ListObject<OrderInfoObject>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrders([FromRoute] Guid id, [FromQuery] PaginationObject page)
        {
            return Ok(await _mediator.Send(new CustomerOrdersQuery
            {
                Id = id,
                Pagination = page,
            }));
        }

        [HttpPost("{id}/orders")]
        [Authorize(Roles = "Customer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateOrder([FromRoute] Guid id, [FromBody, Required] OrderObject order)
        {
            await _mediator.Send(new CreateOrderCommand
            {
                 CustomerId = id,
                 Products = order.Products,
                 AddressId = order.AddressId,
                 CardId = order.CardId,
            });
            return NoContent();
        }

        [HttpGet("{id}/cards")]
        [Authorize(Roles = "Customer")]
        [ProducesResponseType(typeof(ListObject<CardInfoObject>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCards([FromRoute] Guid id, [FromQuery] PaginationObject page)
        {
            return Ok(await _mediator.Send(new CustomerCardsQuery
            {
                Id = id,
                Pagination = page,
            }));
        }

        [HttpPost("{id}/cards")]
        [Authorize(Roles = "Customer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SaveCard([FromRoute] Guid id, [FromBody, Required] CardObject card)
        {
            await _mediator.Send(new SaveCardCommand
            {
                CustomerId = id,
                CardName = card.CardName,
                CardNumber = card.CardNumber,
                Cvc = card.Cvc,
                ExpireMonth = card.ExpireMonth,
                ExpireYear = card.ExpireYear,
            });

            return NoContent();
        }
    }
}

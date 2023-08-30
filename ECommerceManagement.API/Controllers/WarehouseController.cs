using ECommerceManagement.API.Commands.Stock;
using ECommerceManagement.API.DTOs;
using ECommerceManagement.API.Queries.Stock;
using ECommerceManagement.API.Queries.Warehouse;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ECommerceManagement.API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class WarehouseController : BaseController
    {
        public WarehouseController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        [ProducesResponseType(typeof(WarehouseInfoObject), 200)]
        public async Task<IActionResult> Info([FromQuery] PaginationObject page)
        {
            return Ok(await _mediator.Send(new WarehouseInfoQuery
            {
                Pagination = page
            }));
        }

        [HttpGet("stocks/{id}")]
        [ProducesResponseType(typeof(ListObject<StockInfoObject>), 200)]
        public async Task<IActionResult> StockProducts([FromRoute] Guid id, [FromQuery] PaginationObject page)
        {
            return Ok(await _mediator.Send(new WarehouseStocksQuery
            {
                CategoryId = id,
                Pagination = page,
            }));
        }

        [HttpPost("stocks")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateStock([FromBody, Required] StockObject stock)
        {
            var s = await _mediator.Send(new CreateWarehouseStockCommand
            {
                ProductId = stock.ProductId,
                Amount = stock.Amount,
            });

            return Created($"", s);
        }

        [HttpPut("stocks/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddStock([FromRoute] Guid id, [FromBody, Required] int amount)
        {
            await _mediator.Send(new AddWarehouseStockCommand
            {
                Id = id,
                Amount = amount,
            });
            return NoContent();
        }

        //[HttpDelete("stocks/{id}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> DeleteStock([FromRoute] Guid id)
        //{
        //    await _mediator.Send(new DeleteWarehouseStockCommand
        //    {
        //        Id = id
        //    });
        //    return NoContent();
        //}
    }
}

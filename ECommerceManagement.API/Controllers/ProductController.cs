using ECommerceManagement.API.Commands.Product;
using ECommerceManagement.API.DTOs;
using ECommerceManagement.API.Queries.Product;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ECommerceManagement.API.Controllers
{
    public class ProductController : BaseController
    {
        public ProductController(IMediator mediator) : base(mediator) { }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ProductInfoObject), StatusCodes.Status200OK)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Info([FromRoute] Guid id)
        {
            return Ok(await _mediator.Send(new ProductInfoQuery { Id = id }));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid id,
                                                       [FromBody, Required] ProductObject product)
        {
            await _mediator.Send(new UpdateProductCommand
            {
                Id = id,
                Name = product.Name,
                Price = product.Price,
            });
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
        {
            await _mediator.Send(new DeleteProductCommand
            {
                Id = id,
            });
            return NoContent();
        }
    }
}

using ECommerceManagement.API.Commands.Category;
using ECommerceManagement.API.Commands.Product;
using ECommerceManagement.API.DTOs;
using ECommerceManagement.API.Queries.Category;
using ECommerceManagement.API.Queries.Product;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ECommerceManagement.API.Controllers
{
    public class CategoryController : BaseController
    {
        public CategoryController(IMediator mediator) : base(mediator) { }

        [HttpGet("")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ListObject<CategoryInfoObject>), 200)]
        public async Task<IActionResult> GetCategories([FromQuery] PaginationObject page)
        {
            return Ok(await _mediator.Send(new GetCategoriesQuery
            {
                Pagination = page,
            }));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CategoryInfoObject), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Info([FromRoute] Guid id)
        {
            return Ok(await _mediator.Send(new CategoryInfoQuery { Id = id }));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateCategory([FromBody, Required] CategoryObject category)
        {
            var res = await _mediator.Send(new CreateCategoryCommand
            {
                Name = category.Name,
            });
            return Created($"/api/Category/{res.Id}", res);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, [FromBody, Required] CategoryObject category)
        {
            await _mediator.Send(new UpdateCategoryCommand
            {
                Id = id,
                Name = category.Name,
            });
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            await _mediator.Send(new DeleteCategoryCommand
            {
                Id = id
            });
            return NoContent();
        }

        [HttpGet("{id}/products")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ListObject<ProductInfoObject>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProducts([FromRoute] Guid id, [FromQuery] PaginationObject page)
        {
            return Ok(await _mediator.Send(new CategoryProductsQuery
            {
                Id = id,
                Pagination = page
            }));
        }

        [HttpPost("{id}/products")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateProduct([FromRoute] Guid id,
                                                       [FromBody, Required] ProductObject product)
        {
            var res = await _mediator.Send(new CreateProductCommand
            {
                CategoryId = id,
                Name = product.Name,
                Price = product.Price
            });
            return Created($"/api/Product/{res.Id}", res);
        }
    }
}

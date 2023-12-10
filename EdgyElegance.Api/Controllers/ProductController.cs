using EdgyElegance.Application.Constants;
using EdgyElegance.Application.Features.Commands.Product.CreateProductCommand;
using EdgyElegance.Application.Features.Commands.Product.DeleteProductCommand;
using EdgyElegance.Application.Features.Commands.Product.UpdateProductCommand;
using EdgyElegance.Application.Features.Queries.Product.GetProductDetailsQuery;
using EdgyElegance.Application.Features.Queries.Product.GetProductsPaginatedQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EdgyElegance.Api.Controllers {
    [Route("/api/v1/product")]
    public class ProductController : Controller {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-product")]
        [AllowAnonymous]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task <IActionResult> GetProductDetails([FromQuery] int id) {
            var query = new GetProductDetailsQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("get-products/{page:int}")]
        [AllowAnonymous]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetProducts(int page = 1, [FromQuery] int count = 10, [FromQuery] string name = "") {
            var query = new GetProductsPaginatedQuery {
                Page = page,
                Size = count,
                Name = name
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        [Route("create-product")]
        [Authorize(Roles = RoleConstants.Admin)]
        [ProducesResponseType((int) HttpStatusCode.Created)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task <IActionResult> CreteProduct([FromForm] CreateProductCommand command) {
            var result = await _mediator.Send(command);
            return Created(Request.GetEncodedUrl().ToString(), result);
        }

        [HttpPut]
        [Route("update-product")]
        [Authorize(Roles = RoleConstants.Admin)]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand command) {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete]
        [Route("delete-product")]
        [Authorize(Roles = RoleConstants.Admin)]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteProduct([FromQuery] int id) {
            var command = new DeleteProductCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}

using EdgyElegance.Application.Features.Commands.Category.CreateCategoryCommand;
using EdgyElegance.Application.Features.Commands.Category.DeleteCategoryCommand;
using EdgyElegance.Application.Features.Commands.Category.UpdateCategoryCommand;
using EdgyElegance.Application.Features.Queries.Category.GetCategoryQuery.GetCategoriesPaginatedQuery;
using EdgyElegance.Application.Features.Queries.Category.GetCategoryQuery.GetCategoryDetailsQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EdgyElegance.Api.Controllers {
    [Route("/api/category")]
    public class CategoryController : Controller {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator) {
            _mediator = mediator;
        }

        [Route("get-categories-paginated")]
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetCategoriesPaginated([FromQuery] string name, [FromQuery] int page = 1, [FromQuery] int count = 10) {
            var query = new GetCategoriesPaginatedQuery { Name = name, Page = page, Count = count };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [Route("get-category-details")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCategoryDetails([FromQuery] int id) {
            var query = new GetCategoryDetailsQuery(id);
            var category = await _mediator.Send(query);
            return Ok(category);
        }

        [Route("post-category")]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command) {
            int response = await _mediator.Send(command);
            return Ok(response);
        }

        [Route("delete-category")]
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteCategory([FromQuery] int id) {
            var command = new DeleteCategoryCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }

        [Route("update-category")]
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryCommand command) {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}

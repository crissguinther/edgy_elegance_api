using EdgyElegance.Application.Constants;
using EdgyElegance.Application.Features.Commands.Image.UpdateProductImagesCommand;
using EdgyElegance.Application.Features.Queries.Images.GetProductImageQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EdgyElegance.Api.Controllers;

[Route("/api/v1/image")]
public class ImageController : Controller {
    private readonly IMediator _mediator;

    public ImageController(IMediator mediator) {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-product-image")]
    [AllowAnonymous]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    public async Task<FileStreamResult> GetProductImage([FromQuery] int id) {
        var query = new GetProductThumbnailImageQuery(id);
        var file = await _mediator.Send(query);

        return new FileStreamResult(file, "image/png");
    }

    [HttpGet]
    [Route("get-product-thumbnail-image")]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<FileStreamResult> GetProductThumbnailImage([FromQuery] int id) {
        var query = new GetProductThumbnailImageQuery(id);
        var file = await _mediator.Send(query);

        return new FileStreamResult(file, "image/png");
    }

    [HttpPut]
    [Route("update-product-images")]
    [Authorize(Roles = RoleConstants.Admin)]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateProductImage([FromForm] UpdateProductImagesCommand command) {
        await _mediator.Send(command);
        return Ok();
    }
}

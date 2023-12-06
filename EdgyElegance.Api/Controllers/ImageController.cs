using EdgyElegance.Application.Constants;
using EdgyElegance.Application.Features.Commands.Image.UpdateProductImagesCommand;
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

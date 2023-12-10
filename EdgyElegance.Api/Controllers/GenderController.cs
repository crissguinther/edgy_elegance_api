using EdgyElegance.Application.Constants;
using EdgyElegance.Application.Features.Commands.Gender.Commands.CreateGenderCommand;
using EdgyElegance.Application.Features.Commands.Gender.Commands.DeleteGenderCommand;
using EdgyElegance.Application.Features.Commands.Gender.Commands.UpdateGenderCommand;
using EdgyElegance.Application.Features.Queries.Gender.GetGenderDetailsQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EdgyElegance.Api.Controllers;

[Route("/api/v1/gender")]
public class GenderController : Controller {
    private readonly IMediator _mediator;

    public GenderController(IMediator mediator) {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-gender-details")]
    [Authorize (Roles = RoleConstants.Admin)]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetGenderDetails([FromQuery] int id) {
        var query = new GetGenderDetailsQuery(id);
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpPost]
    [Route("create-gender")]
    [Authorize (Roles = RoleConstants.Admin)]
    [ProducesResponseType(201)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> CreateGender([FromBody] CreateGenderCommand command) {
        int result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete]
    [Route("delete-gender")]
    [Authorize (Roles = RoleConstants.Admin)]
    [ProducesResponseType((int) HttpStatusCode.NoContent)]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    public async Task<IActionResult> DeleteGender([FromQuery] int id) {
        var command = new DeleteGenderCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut]
    [Route("update-gender")]
    [Authorize (Roles = RoleConstants.Admin)]
    [ProducesResponseType ((int) HttpStatusCode.NoContent)]
    [ProducesResponseType ((int) HttpStatusCode.NotFound)]
    [ProducesResponseType ((int) HttpStatusCode.BadRequest)]
    public async Task<IActionResult> UpdateGender([FromBody] UpdateGenderCommand command) {
        await _mediator.Send(command);
        return NoContent();
    }
}

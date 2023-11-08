using EdgyElegance.Application.Interfaces.Services;
using EdgyElegance.Application.Models.RequestModels;
using EdgyElegance.Application.Models.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EdgyElegance.Api.Controllers
{
    [Route("/api/v1/users")]
    public class UserController : Controller {
        private readonly IUserService _userService;

        public UserController(IUserService userService) {
            _userService = userService;
        }

        [HttpPost]
        [Route("create-admin-user")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAdminUser([FromBody] CreateUserRequest userModel) {
            if (!ModelState.IsValid) {
                return BadRequest(new BadRequestResponse(ModelState));
            }

            var response = await _userService.CreateUserAsync(userModel, "Admin");

            return Ok(response);
        }

        [HttpPost]
        [Route("create-customer")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateCustomerUser([FromBody] CreateUserRequest userModel) {
            if (!ModelState.IsValid) {
                return BadRequest(new BadRequestResponse(ModelState));
            }

            var response = await _userService.CreateUserAsync(userModel, "Customer");

            return Ok(response);
        }
    }
}

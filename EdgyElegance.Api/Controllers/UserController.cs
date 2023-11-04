using EdgyElegance.Application.Interfaces.Services;
using EdgyElegance.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace EdgyElegance.Api.Controllers {
    [Route("/api/v1/users")]
    public class UserController : Controller {
        private readonly IUserService _userService;

        public UserController(IUserService userService) {
            _userService = userService;
        }

        [HttpGet]
        [Route("create-user")]
        public async Task<IActionResult> CreateUser([FromBody] UserModel userModel) {
            var response = await _userService.CreateUserAsync(userModel);

            return Ok(response);
        }
    }
}

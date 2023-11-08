using EdgyElegance.Application.Config;
using EdgyElegance.Application.Interfaces.Services;
using EdgyElegance.Application.Models.RequestModels;
using EdgyElegance.Application.Models.ResponseModels;
using EdgyElegance.Identity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EdgyElegance.Api.Controllers {
    [Route("/api/v1/auth")]
    public class AuthController : Controller {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService) {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost]
        [Route("get-token")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTokens([FromBody] LoginRequest user) {
            if (!ModelState.IsValid) 
                return BadRequest(new BadRequestResponse(ModelState));

            if (!_userService.UserExists(u => u.Email == user.Email))
                return BadRequest(new BadRequestResponse {
                    Errors = new List<string> { "User not found" }
                });

            ApplicationUser applicationUser = _userService.GetUser(u => u.Email == user.Email)!;
            string token = await _authService.CreateTokenAsync(applicationUser);
            string refreshToken = _authService.CreateRefreshToken(applicationUser);

            AddRefreshTokenCookie(refreshToken);

            return Ok(token);
        }

        [HttpGet]
        [Route("refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken() {
            KeyValuePair<string, string>? cookie = Request.Cookies.FirstOrDefault(x => x.Key == "EdgyEleganceRefreshToken");

            if (cookie is null) return BadRequest(new BadRequestResponse { Errors = new List<string> { "Token not found"} });

            var token = await _authService.RefreshUserToken(cookie.Value.Value);

            AddRefreshTokenCookie(token.RefreshToken);

            return Ok(token.RefreshToken);
        }

        private void AddRefreshTokenCookie(string refreshToken) {
            Response.Cookies.Append("EdgyEleganceRefreshToken", refreshToken, new CookieOptions {
                Expires = Config.RefreshTokenExpiration,
                IsEssential = true,
                HttpOnly = true,
                SameSite = SameSiteMode.Lax,
                Secure = true
            });
        }
    }
}

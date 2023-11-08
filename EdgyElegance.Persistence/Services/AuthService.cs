using EdgyElegance.Application.Config;
using EdgyElegance.Application.Interfaces;
using EdgyElegance.Application.Interfaces.Services;
using EdgyElegance.Application.Models.ResponseModels;
using EdgyElegance.Identity.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EdgyElegance.Persistence.Services {
    public class AuthService : IAuthService {
        public IUnitOfWork UnitOfWork { get; private set; }

        public AuthService(IUnitOfWork unitOfWork) {
            UnitOfWork = unitOfWork;
        }

        public async Task<string> CreateTokenAsync(ApplicationUser user) {
            var roles = await UnitOfWork.UserRepository.GetUserRoles(user);

            List<Claim> claims = new() {
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}")
            };

            roles.ToList().ForEach(r => claims.Add(new Claim(ClaimTypes.Role, r)));
                        
            string jwtSecret = Environment.GetEnvironmentVariable("JwtSecret")!;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(claims: claims, expires: DateTime.UtcNow.AddMinutes(15), signingCredentials: credentials);
            string strToken = new JwtSecurityTokenHandler().WriteToken(token);

            return strToken;
        }

        public string CreateRefreshToken(ApplicationUser user) {
            RefreshToken refreshToken = new() {
                ExpiresIn = Config.RefreshTokenExpiration
            };
            UnitOfWork.AuthRepository.CreateRefreshToken(user, refreshToken);
            UnitOfWork.Commit();
            return refreshToken.Token;
        }

        public async Task<TokenResponse> RefreshUserToken(string token) {
            var result = new TokenResponse();
            var refreshToken = UnitOfWork.AuthRepository.GetTokens(x => x.Token == token).FirstOrDefault();

            if (refreshToken is null) {
                result.Errors!.Add("Token not found");
                return result;
            }

            result.Token = await CreateTokenAsync(refreshToken.User!);
            result.RefreshToken = CreateRefreshToken(refreshToken.User!);

            return result;
        }
    }
}

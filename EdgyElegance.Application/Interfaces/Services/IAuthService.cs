using EdgyElegance.Application.Models.ResponseModels;
using EdgyElegance.Identity.Entities;

namespace EdgyElegance.Application.Interfaces.Services {
    public interface IAuthService {
        IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Creates a Json Web Token for the specified <see cref="ApplicationUser"/>
        /// </summary>
        /// <param name="user">The <see cref="ApplicationUser"/></param>
        /// <returns>The generated token if succeeded, <see cref="null"/> otherwise</returns>
        Task<string> CreateTokenAsync(ApplicationUser user);

        /// <summary>
        /// Creates a <see cref="RefreshToken"/> for the specified 
        /// <see cref="ApplicationUser"/>
        /// </summary>
        /// <param name="user">The <see cref="ApplicationUser"/></param>
        /// <returns>The <see cref="TokenResponse"/> with the refresh token</returns>
        string CreateRefreshToken(ApplicationUser user);

        /// <summary>
        /// Refreshes a <see cref="ApplicationUser"/>'s token according to its
        /// token stored in the database
        /// </summary>
        /// <param name="token">The refresh token</param>
        /// <returns>A task that resolves in the refresh token</returns>
        Task<TokenResponse> RefreshUserToken(string token);
    }
}

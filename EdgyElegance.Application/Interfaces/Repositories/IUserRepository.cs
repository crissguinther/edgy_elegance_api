using EdgyElegance.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace EdgyElegance.Application.Interfaces.Repositories {
    public interface IUserRepository
    {
        /// <summary>
        /// Creates a new <see cref="ApplicationUser"/> async
        /// </summary>
        /// <param name="entity">The <see cref="ApplicationUser"/> to be store</param>
        /// <returns>The <see cref="IdentityResult"/> of the request</returns>
        Task<IdentityResult> CreateAsync(ApplicationUser user, string password);

        /// <summary>
        /// Finds a <see cref="ApplicationUser"/> through its email
        /// </summary>
        /// <param name="email">The email to look for</param>
        /// <returns>The found <see cref="ApplicationUser"/></returns>
        Task<ApplicationUser?> GetByEmailAsync(string email);

        /// <summary>
        /// Adds a <see cref="ApplicationUser"/> to a <see cref="IdentityRole"/>
        /// </summary>
        /// <param name="user">The <see cref="ApplicationUser"/></param>
        /// <param name="role">The role's name</param>
        /// <returns>The <see cref="IdentityResult"/> generated</returns>
        Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role);
    }
}

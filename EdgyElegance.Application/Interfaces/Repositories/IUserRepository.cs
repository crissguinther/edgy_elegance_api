using EdgyElegance.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

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

        /// <summary>
        /// Checks if a <see cref="ApplicationUser"/> exists through its email
        /// </summary>
        /// <param name="email">The email to look for</param>
        /// <returns><see cref="true"/> if exists, <see cref="false"/> otherwise</returns>
        Task<bool> UserExists(string email);

        /// <summary>
        /// Checks if a password is valid for a given <see cref="ApplicationUser"/>
        /// </summary>
        /// <param name="user">The <see cref="ApplicationUser"/></param>
        /// <param name="password">The provided password</param>
        /// <returns><see cref="true"/> if valid, <see cref="false"/> otherwise</returns>
        Task<bool> IsPasswordValid(ApplicationUser user, string password);

        /// <summary>
        /// Gets an <see cref="IList{T}"/> with the <see cref="IdentityRole"/> s
        /// names that the <see cref="ApplicationUser"/> has
        /// </summary>
        /// <param name="user">The <see cref="ApplicationUser"/></param>
        /// <returns>
        /// A <see cref="Task"/> that resolves into an <see cref="IList{T}"/> of
        /// or type <see cref="string"/> or <see cref="null"/> if not found
        /// </returns>
        Task<IList<string>> GetUserRoles(ApplicationUser user);

        /// <summary>
        /// Gets an <see cref="ApplicationUser"/>
        /// </summary>
        /// <param name="id">The <see cref="ApplicationUser"/>'s ID</param>
        /// <returns>The <see cref="ApplicationUser"/> or <see cref="null"/> if not found</returns>
        Task<ApplicationUser?> GetByIdAsync(string id);
    }
}

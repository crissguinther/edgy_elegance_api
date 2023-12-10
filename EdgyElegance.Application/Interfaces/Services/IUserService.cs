using EdgyElegance.Application.Models.RequestModels;
using EdgyElegance.Application.Models.ResponseModels;
using EdgyElegance.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace EdgyElegance.Application.Interfaces.Services {
    public interface IUserService {
        /// <summary>
        /// Creates an <see cref="ApplicationUser"/> and adds it to the
        /// <see cref="IdentityRole"/> that matches the role passed as parameter
        /// </summary>
        /// <param name="userModel"></param>
        /// <param name="role"></param>
        /// <returns>
        /// A <see cref="Task"/> that resolves in the generates <see cref="UserResponse"/>
        /// </returns>
        Task<UserResponse> CreateUserAsync(CreateUserRequest userModel, string role);

        /// <summary>
        /// Adds a user to a <see cref="IdentityRole"/> through its role name
        /// </summary>
        /// <param name="email">The <see cref="ApplicationUser"/>'s email</param>
        /// <param name="role">
        /// The role that the <see cref="ApplicationUser"/> will be added
        /// </param>
        /// <param name="saveOnSuccess">
        /// If the changes should be persisted on the database as soon that the request has
        /// succeeded
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> that resolves in the generates <see cref="UserResponse"/>
        /// </returns>
        Task<UserResponse> AddToRoleByEmailAsync(string email, string role, bool saveOnSuccess = false);

        /// <summary>
        /// Gets an <see cref="ApplicationUser"/> through its Id
        /// </summary>
        /// <param name="id">The Id to look for</param>
        /// <returns>The <see cref="ApplicationUser"/> or <see cref="null"/> if not found</returns>
        Task<ApplicationUser?> GetUser(string id);

        /// <summary>
        /// Checks if an <see cref="ApplicationUser"/> exists
        /// </summary>
        /// <param name="email">The email to look for</param>
        /// <returns><see cref="true"/> if exists, false otherwise</returns>
        Task<bool> UserExists(string email);

        /// <summary>
        /// Gets an <see cref="ApplicationUser"/> through its email
        /// </summary>
        /// <param name="email">The email to look for</param>
        /// <returns><see cref="null"/> of not found or the <see cref="ApplicationUser"/> if found</returns>
        Task<ApplicationUser?> GetByEmailAsync(string email);
    }
}

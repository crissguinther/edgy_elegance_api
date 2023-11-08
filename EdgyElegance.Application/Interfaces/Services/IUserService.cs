using EdgyElegance.Application.Interfaces.Repositories;
using EdgyElegance.Application.Models.RequestModels;
using EdgyElegance.Application.Models.ResponseModels;
using EdgyElegance.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

namespace EdgyElegance.Application.Interfaces.Services
{
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
        /// Gets an <see cref="ApplicationUser"/> through the passed expression
        /// </summary>
        /// <param name="predicate">The <see cref="Expression"/> to be used</param>
        /// <returns>The <see cref="ApplicationUser"/> or <see cref="null"/> if not found</returns>
        ApplicationUser? GetUser(Expression<Func<ApplicationUser, bool>> predicate);

        /// <summary>
        /// Checks if an <see cref="ApplicationUser"/> exists
        /// </summary>
        /// <param name="predicate">The <see cref="Expression"/> to be used</param>
        /// <returns><see cref="true"/> if exists, false otherwise</returns>
        bool UserExists(Expression<Func<ApplicationUser, bool>> predicate);
    }
}

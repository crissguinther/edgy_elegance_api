using EdgyElegance.Application.Interfaces.Repositories;
using EdgyElegance.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace EdgyElegance.Persistence.Repositories {
    public class UserRepository : IUserRepository {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(UserManager<ApplicationUser> userManager) {
            _userManager  = userManager;
        }

        public Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role) {
            return _userManager.AddToRoleAsync(user, role);
        }

        public Task<IdentityResult> CreateAsync(ApplicationUser user, string password) {
            return _userManager.CreateAsync(user, password);
        }

        public Task<ApplicationUser?> GetByEmailAsync(string email) {
            return _userManager.FindByEmailAsync(email);
        }
    }
}

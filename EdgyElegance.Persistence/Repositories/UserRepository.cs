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

        public Task<ApplicationUser?> GetByIdAsync(string id)
            => _userManager.FindByIdAsync(id);

        public Task<ApplicationUser?> GetByEmailAsync(string email) {
            return _userManager.FindByEmailAsync(email);
        }

        public Task<IList<string>> GetUserRoles(ApplicationUser user) 
            => _userManager.GetRolesAsync(user);
        

        public async Task<bool> IsPasswordValid(ApplicationUser user, string password)
            => await _userManager.CheckPasswordAsync(user, password);

        public async Task<bool> UserExists(string email) {
            ApplicationUser? user = await _userManager.FindByEmailAsync(email);

            return user is not null;
        }            
    }
}

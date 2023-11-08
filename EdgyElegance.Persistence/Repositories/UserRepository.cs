using EdgyElegance.Application.Interfaces.Repositories;
using EdgyElegance.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

namespace EdgyElegance.Persistence.Repositories {
    public class UserRepository : IUserRepository {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBaseRepository<ApplicationUser> _baseRepository;

        public UserRepository(UserManager<ApplicationUser> userManager, IBaseRepository<ApplicationUser> baseRepository) {
            _userManager  = userManager;
            _baseRepository = baseRepository;
        }

        public Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role) {
            return _userManager.AddToRoleAsync(user, role);
        }

        public Task<IdentityResult> CreateAsync(ApplicationUser user, string password) {
            return _userManager.CreateAsync(user, password);
        }

        public ApplicationUser? Get(Expression<Func<ApplicationUser, bool>> expression) 
            => _baseRepository.Get(expression);

        public Task<ApplicationUser?> GetByEmailAsync(string email) {
            return _userManager.FindByEmailAsync(email);
        }

        public Task<IList<string>> GetUserRoles(ApplicationUser user) 
            => _userManager.GetRolesAsync(user);
        

        public async Task<bool> IsPasswordValid(ApplicationUser user, string password)
            => await _userManager.CheckPasswordAsync(user, password);

        public bool UserExists(Expression<Func<ApplicationUser, bool>> predicate)
            => _baseRepository.Exists(predicate);
    }
}

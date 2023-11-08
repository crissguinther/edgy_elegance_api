using EdgyElegance.Application.Interfaces.Repositories;
using EdgyElegance.Identity;
using EdgyElegance.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EdgyElegance.Persistence.Repositories {
    public class AuthRepository : IAuthRepository {
        private readonly EdgyEleganceIdentityContext _context;

        public AuthRepository(EdgyEleganceIdentityContext context) {
            _context = context;
        }

        public void CreateRefreshToken(ApplicationUser user, RefreshToken refreshToken) {
            user.RefreshTokens.Add(refreshToken);
        }

        public IQueryable<RefreshToken> GetTokens(Expression<Func<RefreshToken, bool>> predicate)
            => _context.RefreshTokens
                .Include(rt => rt.User)
                .Where(predicate);
    }
}

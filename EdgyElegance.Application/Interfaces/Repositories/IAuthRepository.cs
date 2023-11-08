using EdgyElegance.Identity.Entities;
using System.Linq.Expressions;

namespace EdgyElegance.Application.Interfaces.Repositories {
    public interface IAuthRepository {
        /// <summary>
        /// Creates a <see cref="ApplicationUser"/> token and stores in the
        /// database
        /// </summary>
        /// <param name="user">The <see cref="ApplicationUser"/></param>
        /// <param name="token">The <see cref="ApplicationUser"/>'s token</param>
        void CreateRefreshToken(ApplicationUser user, RefreshToken refreshToken);

        /// <summary>
        /// Gets the <see cref="RefreshToken"/>s based on the passed
        /// <see cref="Expression"/>
        /// </summary>
        /// <param name="predicate">The <see cref="Expression"/> to be used</param>
        /// <returns>The generated <see cref="IQueryable"/></returns>
        IQueryable<RefreshToken> GetTokens(Expression<Func<RefreshToken, bool>> predicate);
    }
}

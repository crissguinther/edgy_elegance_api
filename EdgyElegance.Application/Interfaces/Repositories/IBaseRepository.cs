using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace EdgyElegance.Application.Interfaces.Repositories {
    public interface IBaseRepository<T> where T : class
    {
        /// <summary>
        /// Creates a database entry async
        /// </summary>
        /// <param name="entity">The entity to be stored</param>
        /// <returns>The entity</returns>
        void Create(T entity);

        /// <summary>
        /// Updates an entity async
        /// </summary>
        /// <param name="entity">The entity to be updated</param>
        /// <returns>The updated entry</returns>
        EntityEntry<T> Update(T entity);

        /// <summary>
        /// Deletes an entity async
        /// </summary>
        /// <param name="entity">The entity to be deleted</param>
        void DeleteAsync(T entity);

        /// <summary>
        /// Gets one entry according to the <see cref="Expression"/> passed
        /// </summary>
        /// <param name="expression">The <see cref="Expression"/> to search for</param>
        /// <returns>The found entry, null otherwise</returns>
        T? Get(Expression<Func<T, bool>> expression, Expression<Func<T, object>>[]? includes = null);

        /// <summary>
        /// Looks for all the entities that matches the specified <see cref="Expression"/>
        /// </summary>
        /// <param name="expression">The <see cref="Expression"/> to be used</param>
        /// <returns>The found entities, null otherwise</returns>
        IQueryable<T> GetMany(Expression<Func<T, bool>> expression, Expression<Func<T, object>>[]? includes = null);

        /// <summary>
        /// Checks if the entity exists according to the <see cref="Expression"/>
        /// </summary>
        /// <param name="expression">The <see cref="Expression"/> to be used</param>
        /// <returns><see cref="true"/> if exists, <see cref="false"/> otherwise</returns>
        bool Exists(Expression<Func<T, bool>> expression);
    }
}

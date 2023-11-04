using System.Linq.Expressions;

namespace EdgyElegance.Application.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        /// <summary>
        /// Creates a database entry async
        /// </summary>
        /// <param name="entity">The entity to be stored</param>
        /// <returns>The entity</returns>
        Task<T> CreateAsync(T entity);

        /// <summary>
        /// Updates an entity async
        /// </summary>
        /// <param name="entity">The entity to be updated</param>
        /// <returns>The updated entry</returns>
        Task<T> UpdateAsync(T entity);

        /// <summary>
        /// Deletes an entity async
        /// </summary>
        /// <param name="entity">The entity to be deleted</param>
        Task DeleteAsync(T entity);

        /// <summary>
        /// Gets one entry according to the <see cref="Expression"/> passed
        /// </summary>
        /// <param name="expression">The <see cref="Expression"/> to search for</param>
        /// <returns>The found entry, null otherwise</returns>
        Task<T> GetAsync(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Looks for all the entities that matches the specified <see cref="Expression"/>
        /// </summary>
        /// <param name="expression">The <see cref="Expression"/> to be used</param>
        /// <returns>The found entities, null otherwise</returns>
        Task<IQueryable<T>> GetManyAsync(Expression<Func<T, bool>> expression);
    }
}

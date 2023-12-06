using EdgyElegance.Domain.Entities;
using System.Linq.Expressions;

namespace EdgyElegance.Application.Contracts.Persistence;

public interface IBaseRepository<T> where T : BaseEntity {
    /// <summary>
    /// Gets the entity async
    /// </summary>
    /// <param name="id">The entity's ID</param>
    /// <returns>The entity if found, null otherwise</returns>
    Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);

    /// <summary>
    /// Adds a new entry to the database async
    /// </summary>
    /// <param name="entity">The entity to be added</param>
    /// <returns>The added entity</returns>
    Task<T> AddAsync(T entity);

    /// <summary>
    /// Gets the entitys paginated, given a page number and size
    /// </summary>
    /// <param name="page">The page's number</param>
    /// <param name="pageSize">The page's size</param>
    /// <param name="filter">A filter to be used</param>
    /// <returns>A <see cref="List{T}"/> with the results found</returns>
    List<T> GetPaginated(int page, int pageSize, object? filter = null, params Expression<Func<T, object>>[] includes);

    /// <summary>
    /// Deletes an entity from the database
    /// </summary>
    /// <param name="entity">The entity to be deleted</param>
    void Delete(T entity);

    /// <summary>
    /// Updates an entity entry on the database
    /// </summary>
    /// <param name="entity">The entity to be updated</param>
    void Update(T entity);
}

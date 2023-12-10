using EdgyElegance.Domain.Entities;
using System.Linq.Expressions;

namespace EdgyElegance.Application.Contracts.Persistence;
public interface ICategoryRepository {
    /// <summary>
    /// Verifies if a category already exists in the database
    /// through its name
    /// </summary>
    /// <param name="name">The name to look for</param>
    /// <returns>A <see cref="Task"/> that resolves in a <see cref="bool"/> value</returns>
    Task<bool> CategoryExistsAsync(string name);

    /// <summary>
    /// Adds a new entry async
    /// </summary>
    /// <param name="category">The <see cref="Category"/> to be added</param>
    /// <returns>A <see cref="Task"/> that resolves in the <see cref="Category"/> Id's</returns>
    Task<Category> AddAsync(Category category);

    Task<Category?> GetAsync(int id);

    List<Category> GetPaginated(int page, int count, object? filter = null);

    Task<List<Category>> GetManyAsync(Expression<Func<Category, bool>> expression);

    void Delete(Category category);

    void Update(Category category);
}

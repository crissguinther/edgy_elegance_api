using EdgyElegance.Domain.Entities;
using System.Linq.Expressions;

namespace EdgyElegance.Application.Contracts.Persistence;
public interface IProductRepository {
    /// <summary>
    /// Checks if a <see cref="Product"/> exists on the database through its name
    /// </summary>
    /// <param name="productName">The name to search for</param>
    /// <returns>True if exists, false otherwise</returns>
    Task<bool> ExistsAsync(string productName);

    Task<Product?> FindByIdAsync(int id, params Expression<Func<Product, object>>[] includes);

    void Update(Product product);

    void Delete(Product product);

    Task<List<Product>> GetProductsPaginated(int page = 1, int count = 10, object? filter = null, params Expression<Func<Product, object>>[] includes);

    Task<Product> AddAsync(Product product);
}

using EdgyElegance.Application.Contracts.Persistence;
using EdgyElegance.Domain.Entities;
using EdgyElegance.Persistence.DatabaseContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EdgyElegance.Persistence.Repositories;

internal class ProductRepository : IProductRepository {
    private readonly ApplicationContext _context;
    private readonly IBaseRepository<Product> _baseRepository;

    public ProductRepository(ApplicationContext context) {
        _context = context;
        _baseRepository = new BaseRepository<Product>(_context);
    }

    public async Task<Product> AddAsync(Product product) {
        return await _baseRepository.AddAsync(product);
    }

    public void Delete(Product product) {
        _baseRepository.Delete(product);
    }

    public async Task<bool> ExistsAsync(string productName) {
        return await _context.Products.FirstOrDefaultAsync(p => p.Name == productName) is not null;
    }

    public Task<Product?> FindByIdAsync(int id, params Expression<Func<Product, object>>[] includes) {
        return _baseRepository.GetByIdAsync(id, includes);
    }

    public Task<List<Product>> GetProductsPaginated(int page = 1, int count = 10, object? filter = null, params Expression<Func<Product, object>>[] includes) {
        return Task.FromResult(_baseRepository.GetPaginated(page, count, filter, includes));
    }

    public void Update(Product product) {
        _baseRepository.Update(product);
    }
}

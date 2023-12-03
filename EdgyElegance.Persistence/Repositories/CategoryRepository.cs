using EdgyElegance.Application.Contracts.Persistence;
using EdgyElegance.Domain.Entities;
using EdgyElegance.Persistence.DatabaseContexts;
using Microsoft.EntityFrameworkCore;

namespace EdgyElegance.Persistence.Repositories;
public class CategoryRepository : ICategoryRepository {
    private readonly ApplicationContext _context;
    private readonly IBaseRepository<Category> _baseRepository;

    public CategoryRepository(ApplicationContext context) {
        _context = context;
        _baseRepository = new BaseRepository<Category>(context);
    }

    public async Task<Category> AddAsync(Category category) {
        return await _baseRepository.AddAsync(category);
    }

    public Task<bool> CategoryExistsAsync(string name) {
        return _context.Categories.AnyAsync(c => c.Name == name);
    }

    public void Delete(Category category) {
        _baseRepository.Delete(category);
    }

    public async Task<Category?> GetAsync(int id) {
        return await _baseRepository.GetByIdAsync(id);
    }

    public void Update(Category category) {
        _baseRepository.Update(category);
    }

    public List<Category> GetPaginated(int page, int count, object? filter = null) {
        return _baseRepository.GetPaginated(page, count, filter);
    }
}

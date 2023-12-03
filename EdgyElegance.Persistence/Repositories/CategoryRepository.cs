using EdgyElegance.Application.Contracts.Persistence;
using EdgyElegance.Domain.Entities;
using EdgyElegance.Persistence.DatabaseContexts;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EdgyElegance.Persistence.Repositories;
public class CategoryRepository : ICategoryRepository {
    private readonly ApplicationContext _context;

    public CategoryRepository(ApplicationContext context) {
        _context = context;
    }

    public async Task<Category> AddAsync(Category category) {
        var result = await _context.Categories.AddAsync(category);
        return result.Entity;
    }

    public Task<bool> CategoryExistsAsync(string name) {
        return _context.Categories.AnyAsync(c => c.Name == name);
    }

    public void Delete(Category category) {
        _context.Categories.Remove(category);
    }

    public async Task<Category?> GetAsync(int id) {
        return await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
    }

    public void Update(Category category) {
        _context.Categories.Update(category);
    }

    public List<Category> GetPaginated(int page, int count, object? filter = null) {
        IQueryable<Category> query = filter is not null ? Find(filter) : _context.Categories;
        int skip = (page - 1) * count;
        List<int> idsToFetch = query.Select(c => c.Id).Skip(skip).Take(count).ToList();
        IEnumerable<Category> categories = _context.Categories.Where(c => idsToFetch.Contains(c.Id));
        return categories.OrderBy(c => idsToFetch.IndexOf(c.Id)).ToList();
    }

    private IQueryable<Category> Find(object filter) {
        IQueryable<Category> query = _context.Categories;
        var filterProperties = filter.GetType().GetProperties();
        var categoryProperties = typeof(Category).GetType().GetProperties();
        var commonProperties = filterProperties.Where(fp => categoryProperties.Any(cp => cp.Name == fp.Name)).ToList();

        commonProperties.ForEach(c => {
            var property = typeof(Category).GetProperty(c.Name);

            if (property is not null) {
                PropertyInfo? filterProperty = filter.GetType().GetProperty(nameof(property.Name));

                if (filterProperty is not null) {
                    if (property.PropertyType == typeof(string)) {
                        string? value = filterProperty.GetValue(filter)?.ToString();
                        if (!string.IsNullOrEmpty(value)) 
                            query = query.Where(e => EF.Property<string>(e, property.Name).Contains(value!));
                    } else if (property.PropertyType == typeof(int)){
                        var value = Convert.ToInt32(filterProperty.GetValue(filter));
                        query = query.Where(e => EF.Property<int>(e, property.Name) == value);
                    }
                }
            }
        });

        return query;
    }
}

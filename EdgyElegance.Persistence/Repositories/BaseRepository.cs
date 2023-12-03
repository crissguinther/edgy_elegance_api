using EdgyElegance.Application.Contracts.Persistence;
using EdgyElegance.Domain.Entities;
using EdgyElegance.Persistence.DatabaseContexts;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EdgyElegance.Persistence.Repositories;

internal class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity {
    private readonly ApplicationContext _context;

    public BaseRepository(ApplicationContext context) {
        _context = context;
    }

    public async Task<T> AddAsync(T entity) {
        await _context.Set<T>().AddAsync(entity);
        return entity;
    }

    public void Delete(T entity) {
        _context.Set<T>().Remove(entity);
    }

    public async Task<T?> GetByIdAsync(int id) {
        return await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
    }

    public List<T> GetPaginated(int page, int pageSize, object? filter = null) {
        IQueryable<T> query = filter is not null ? Find(filter) : _context.Set<T>();
        int skip = (page - 1) * pageSize;
        List<int> idsToFetch = query.Select(c => c.Id).Skip(skip).Take(pageSize).ToList();
        IEnumerable<T> entities = _context.Set<T>().Where(c => idsToFetch.Contains(c.Id));
        return entities.OrderBy(c => idsToFetch.IndexOf(c.Id)).ToList();
    }

    public void Update(T entity) {
        _context.Set<T>().Update(entity);
    }

    private IQueryable<T> Find(object filter) {
        IQueryable<T> query = _context.Set<T>();
        var filterProperties = filter.GetType().GetProperties();
        var entityProperties = typeof(T).GetType().GetProperties();
        var commonProperties = filterProperties.Where(fp => entityProperties.Any(cp => cp.Name == fp.Name)).ToList();

        commonProperties.ForEach(c => {
            var property = typeof(T).GetProperty(c.Name);

            if (property is not null) {
                PropertyInfo? filterProperty = filter.GetType().GetProperty(nameof(property.Name));

                if (filterProperty is not null) {
                    if (property.PropertyType == typeof(string)) {
                        string? value = filterProperty.GetValue(filter)?.ToString();
                        if (!string.IsNullOrEmpty(value))
                            query = query.Where(e => EF.Property<string>(e, property.Name).Contains(value!));
                    } else if (property.PropertyType == typeof(int)) {
                        var value = Convert.ToInt32(filterProperty.GetValue(filter));
                        query = query.Where(e => EF.Property<int>(e, property.Name) == value);
                    }
                }
            }
        });

        return query;
    }
}

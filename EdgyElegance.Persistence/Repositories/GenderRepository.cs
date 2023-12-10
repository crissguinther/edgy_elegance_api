using EdgyElegance.Application.Contracts.Persistence;
using EdgyElegance.Domain.Entities;
using EdgyElegance.Persistence.DatabaseContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EdgyElegance.Persistence.Repositories;
internal class GenderRepository : IGenderRepository {
    private readonly ApplicationContext _context;
    private readonly IBaseRepository<Gender> _baseRepository;

    public GenderRepository(ApplicationContext context) {
        _context = context;
        _baseRepository = new BaseRepository<Gender>(_context);
    }

    public async Task<Gender> AddAsync(Gender gender) {
        return await _baseRepository.AddAsync(gender);
    }

    public void Delete(Gender gender) {
        _baseRepository.Delete(gender);
    }

    public Task<bool> ExistsAsync(string genderName) {
        return _context.Genders.AnyAsync(c => c.Name == genderName);
    }

    public Task<Gender?> FindByIdAsync(int id) {
        return _baseRepository.GetByIdAsync(id);
    }

    public async Task<List<Gender>> GetManyAsync(Expression<Func<Gender, bool>> predicate) {
        return await _baseRepository.GetManyAsync(predicate);
    }

    public void Update(Gender category) {
        _baseRepository.Update(category);
    }
}

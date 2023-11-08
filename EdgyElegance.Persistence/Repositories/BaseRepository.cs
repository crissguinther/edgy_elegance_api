using EdgyElegance.Application.Interfaces.Repositories;
using EdgyElegance.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace EdgyElegance.Persistence.Repositories {
    public class BaseRepository<T> : IBaseRepository<T> where T : class {
        private readonly EdgyEleganceIdentityContext _context;

        public BaseRepository(EdgyEleganceIdentityContext context) {
            _context = context;
        }

        public void Create(T entity) 
            => _context.Set<T>().Add(entity);

        public void DeleteAsync(T entity) {
            _context.Set<T>().Remove(entity);
        }

        public bool Exists(Expression<Func<T, bool>> expression) 
            => _context.Set<T>().Any(expression);

        public T? Get(Expression<Func<T, bool>> expression, Expression<Func<T, object>>[]? includes = null) {
            var query = _context.Set<T>();

            if (includes is not null) {
                foreach (Expression<Func<T, object>> property in includes) {
                    query.Include<T, object>(property);
                }
            }

            return query.FirstOrDefault(expression);
        } 

        public IQueryable<T> GetMany(Expression<Func<T, bool>> expression, Expression<Func<T, object>>[]? includes = null) {
            var query = _context.Set<T>().Where(expression);
            if (includes is not null) {
                foreach(Expression<Func<T, object>> property in includes) {
                    query.Include<T, object>(property);
                }
            }

            return query;
        }

        public EntityEntry<T> Update(T entity) 
            => _context.Set<T>().Update(entity);
    }
}

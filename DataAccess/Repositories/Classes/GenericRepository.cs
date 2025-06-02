using DataAccess.Contexts;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Classes
{
    public class GenericRepository<TEntity>(LibraryDbContext _libraryDbContext)
        : IGenericRepository<TEntity> where TEntity : class
    {
        public async Task AddAsync(TEntity entity)
              => await _libraryDbContext.Set<TEntity>().AddAsync(entity);

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
              => await _libraryDbContext.Set<TEntity>().AnyAsync(predicate);

        public async Task<IEnumerable<TEntity>> GetAllAsync()
              => await _libraryDbContext.Set<TEntity>().ToListAsync();

        public async Task<TEntity?> GetByIdAsync(int id)
              => await _libraryDbContext.Set<TEntity>().FindAsync(id);

        public void Remove(TEntity entity)
              => _libraryDbContext.Set<TEntity>().Remove(entity);

        public void Update(TEntity entity)
             => _libraryDbContext.Set<TEntity>().Update(entity);
    }
}

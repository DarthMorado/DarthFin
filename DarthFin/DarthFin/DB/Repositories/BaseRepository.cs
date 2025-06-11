using DarthFin.DB.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DarthFin.DB.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task CreateAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveChangesAsync();
    }

    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly Database _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(Database context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);


        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

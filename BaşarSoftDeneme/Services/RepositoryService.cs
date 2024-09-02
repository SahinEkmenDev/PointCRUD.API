using BaşarSoftDeneme.Interfaces;
using BaşarSoftDeneme.Controllers;
using BaşarSoftDeneme.Models;
using Microsoft.EntityFrameworkCore;

namespace BaşarSoftDeneme.Services
{
    public class RepositoryService<T> : IRepositoryService<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public RepositoryService(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> GetByIdAsync(long id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            var keyProperty = _context.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties.First();
            var keyValue = keyProperty.PropertyInfo.GetValue(entity);
            var existingEntity = await _dbSet.FindAsync(keyValue);

            if (existingEntity != null)
            {
                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Güncellenecek kayıt bulunamadı.");
            }
        }

        public async Task DeleteAsync(long id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}

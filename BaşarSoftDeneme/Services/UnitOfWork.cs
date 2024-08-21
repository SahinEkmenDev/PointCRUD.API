using BaşarSoftDeneme.Interfaces;
using BaşarSoftDeneme.Models;

namespace BaşarSoftDeneme.Services
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IRepositoryService<T> Repository<T>() where T : class
        {
            return new RepositoryService<T>(_context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}

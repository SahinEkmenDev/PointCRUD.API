using BaşarSoftDeneme.Models;
using BaşarSoftDeneme.Interfaces;
using BaşarSoftDeneme.Controllers;
using BaşarSoftDeneme.Services;




namespace BaşarSoftDeneme.Interfaces
{
    public interface IRepositoryService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(long id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(long id);
    }
}

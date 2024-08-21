namespace BaşarSoftDeneme.Interfaces
{
    public interface IUnitOfWork 
    {

        IRepositoryService<T> Repository<T>() where T : class;
        Task<int> SaveChangesAsync();

    }
}

namespace NextHome.Domain.Interfaces;

public interface IRepository<T> where T : class
{
    Task<int> AddAsync(T entity, CancellationToken cancellationToken);
    Task<T> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
    Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}

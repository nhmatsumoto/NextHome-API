namespace NextHome.Domain.Interfaces;

public interface IRepository<T> where T : class
{
    Task<int> AddAsync(T entity);
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<bool> UpdateAsync(T entity);
    Task<bool> DeleteAsync(int id);
}

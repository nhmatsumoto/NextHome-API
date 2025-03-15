using NextHome.Domain.Entities;

namespace NextHome.Domain.Interfaces;

public interface IUserRepository
{
    Task<int> AddAsync(User user, CancellationToken cancellationToken = default);
    Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(User user, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsByEmail(string email, CancellationToken cancellationToken = default);
}

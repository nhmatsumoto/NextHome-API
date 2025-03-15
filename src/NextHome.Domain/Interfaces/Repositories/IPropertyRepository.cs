using NextHome.Domain.Entities;

namespace NextHome.Domain.Interfaces.Repositories;

public interface IPropertyRepository
{
    Task<IEnumerable<Property>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Property> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<int> AddAsync(Property property, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Property property, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}

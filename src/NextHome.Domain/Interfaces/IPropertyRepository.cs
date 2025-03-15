using NextHome.Domain.Entities;

namespace NextHome.Domain.Interfaces;

public interface IPropertyRepository
{
    Task<IEnumerable<Property>> GetAllAsync(CancellationToken cancellationToken);
    Task<Property> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<int> AddAsync(Property property, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Property property, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}

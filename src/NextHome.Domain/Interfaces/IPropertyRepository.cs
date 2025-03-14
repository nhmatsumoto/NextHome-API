using NextHome.Domain.Entities;

namespace NextHome.Domain.Interfaces;

public interface IPropertyRepository
{
    Task<IEnumerable<Property>> GetAllAsync();
    Task<Property> GetByIdAsync(int id);
    Task<int> AddAsync(Property property);
    Task<bool> UpdateAsync(Property property);
    Task<bool> DeleteAsync(int id);
}

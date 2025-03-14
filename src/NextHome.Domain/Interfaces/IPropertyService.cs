using NextHome.Domain.Entities;

namespace NextHome.Domain.Interfaces;

public interface IPropertyService
{
    Task<IEnumerable<Property>> GetAllPropertiesAsync();
    Task<Property> GetPropertyByIdAsync(int id);
    Task<int> AddPropertyAsync(Property property);
    Task<bool> UpdatePropertyAsync(Property property);
    Task<bool> DeletePropertyAsync(int id);
}

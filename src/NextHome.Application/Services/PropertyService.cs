using NextHome.Domain.Entities;
using NextHome.Domain.Interfaces;

namespace NextHome.Application.Services;

public class PropertyService : IPropertyService
{
    private readonly IRepository<Property> _repository;
    private readonly IPropertyRepository _propertyRepository;

    public PropertyService(IRepository<Property> repository, IPropertyRepository propertyRepository)
    {
        _repository = repository;
        _propertyRepository = propertyRepository;
    }

    public async Task<IEnumerable<Property>> GetAllPropertiesAsync()
    {
        //GET WITH Custom Property Repository *Includes Inner join on query
        return await _propertyRepository.GetAllAsync();
    }

    public async Task<Property> GetPropertyByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<int> AddPropertyAsync(Property property)
    {
        return await _repository.AddAsync(property);
    }

    public async Task<bool> UpdatePropertyAsync(Property property)
    {
        return await _repository.UpdateAsync(property);
    }

    public async Task<bool> DeletePropertyAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}

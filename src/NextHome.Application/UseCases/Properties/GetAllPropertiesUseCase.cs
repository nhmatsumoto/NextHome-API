using NextHome.Application.Interfaces.Properties;
using NextHome.Domain.Entities;
using NextHome.Domain.Interfaces;

namespace NextHome.Application.UseCases.Properties;

public class GetAllPropertiesUseCase : IGetAllPropertiesUseCase
{
    private readonly IPropertyRepository _propertyRepository;

    public GetAllPropertiesUseCase(IPropertyRepository propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }

    public async Task<IEnumerable<Property>> ExecuteAsync()
    {
        return await _propertyRepository.GetAllAsync();
    }
}

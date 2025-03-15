using NextHome.Application.UseCases.Properties.Interfaces;
using NextHome.Domain.Entities;
using NextHome.Domain.Interfaces.Repositories;

namespace NextHome.Application.UseCases.Properties;

public class GetAllPropertiesUseCase : IGetAllPropertiesUseCase
{
    private readonly IPropertyRepository _propertyRepository;

    public GetAllPropertiesUseCase(IPropertyRepository propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }

    public async Task<IEnumerable<Property>> ExecuteAsync(CancellationToken cancellationToken)
    {
        return await _propertyRepository.GetAllAsync(cancellationToken);
    }
}

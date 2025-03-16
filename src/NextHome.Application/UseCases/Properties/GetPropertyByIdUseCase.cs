using NextHome.Application.UseCases.Properties.Interfaces;
using NextHome.Domain.Entities;
using NextHome.Domain.Interfaces.Repositories;

namespace NextHome.Application.UseCases.Properties;

public class GetPropertyByIdUseCase : IGetPropertyByIdUseCase
{
    private readonly IPropertyRepository _propertyRepository;

    public GetPropertyByIdUseCase(IPropertyRepository propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }

    public async Task<Property> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _propertyRepository.GetByIdAsync(id, cancellationToken);
    }
}

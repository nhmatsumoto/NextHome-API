using NextHome.Application.UseCases.Properties.Interfaces;
using NextHome.Domain.Entities;
using NextHome.Domain.Interfaces;

namespace NextHome.Application.UseCases.Properties;

public class UpdatePropertyUseCase : IUpdatePropertyUseCase
{
    private readonly IRepository<Property> _repository;

    public UpdatePropertyUseCase(IRepository<Property> repository)
    {
        _repository = repository;
    }

    public async Task<bool> ExecuteAsync(Property property, CancellationToken cancellationToken)
    {
        return await _repository.UpdateAsync(property, cancellationToken);
    }
}

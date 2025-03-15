using NextHome.Application.UseCases.Properties.Interfaces;
using NextHome.Domain.Entities;
using NextHome.Domain.Interfaces;

namespace NextHome.Application.UseCases.Properties;

public class CreatePropertyUseCase : ICreatePropertyUseCase
{
    private readonly IRepository<Property> _repository;

    public CreatePropertyUseCase(IRepository<Property> repository)
    {
        _repository = repository;
    }

    public async Task<int> ExecuteAsync(Property property, CancellationToken cancellationToken)
    {
        return await _repository.AddAsync(property, cancellationToken);
    }
}

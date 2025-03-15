using NextHome.Application.UseCases.Properties.Interfaces;
using NextHome.Domain.Entities;
using NextHome.Domain.Interfaces.Repositories;

namespace NextHome.Application.UseCases.Properties;

public class GetPropertyByIdUseCase : IGetPropertyByIdUseCase
{
    private readonly IRepository<Property> _repository;

    public GetPropertyByIdUseCase(IRepository<Property> repository)
    {
        _repository = repository;
    }

    public async Task<Property> ExecuteAsync(int id, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(id, cancellationToken);
    }
}

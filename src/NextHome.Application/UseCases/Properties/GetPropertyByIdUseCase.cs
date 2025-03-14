using NextHome.Application.Interfaces.Properties;
using NextHome.Domain.Entities;
using NextHome.Domain.Interfaces;

namespace NextHome.Application.UseCases.Properties;

public class GetPropertyByIdUseCase : IGetPropertyByIdUseCase
{
    private readonly IRepository<Property> _repository;

    public GetPropertyByIdUseCase(IRepository<Property> repository)
    {
        _repository = repository;
    }

    public async Task<Property> ExecuteAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }
}

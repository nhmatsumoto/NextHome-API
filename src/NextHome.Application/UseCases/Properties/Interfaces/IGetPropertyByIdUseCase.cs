using NextHome.Domain.Entities;

namespace NextHome.Application.UseCases.Properties.Interfaces;

public interface IGetPropertyByIdUseCase
{
    Task<Property> ExecuteAsync(int id, CancellationToken cancellationToken);
}

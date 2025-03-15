using NextHome.Domain.Entities;

namespace NextHome.Application.UseCases.Properties.Interfaces;

public interface IGetAllPropertiesUseCase
{
    Task<IEnumerable<Property>> ExecuteAsync(CancellationToken cancellationToken);
}

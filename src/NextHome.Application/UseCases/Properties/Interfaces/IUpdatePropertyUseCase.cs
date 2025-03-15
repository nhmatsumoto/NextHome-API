using NextHome.Domain.Entities;

namespace NextHome.Application.UseCases.Properties.Interfaces;

public interface IUpdatePropertyUseCase
{
    Task<bool> ExecuteAsync(Property property, CancellationToken cancellationToken);
}

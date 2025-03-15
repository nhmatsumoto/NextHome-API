using NextHome.Domain.Entities;

namespace NextHome.Application.UseCases.Properties.Interfaces;

public interface ICreatePropertyUseCase
{
    Task<int> ExecuteAsync(Property property, CancellationToken cancellationToken);
}

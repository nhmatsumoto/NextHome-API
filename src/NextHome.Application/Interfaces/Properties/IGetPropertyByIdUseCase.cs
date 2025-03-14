using NextHome.Domain.Entities;

namespace NextHome.Application.Interfaces.Properties;

public interface IGetPropertyByIdUseCase
{
    Task<Property> ExecuteAsync(int id);
}

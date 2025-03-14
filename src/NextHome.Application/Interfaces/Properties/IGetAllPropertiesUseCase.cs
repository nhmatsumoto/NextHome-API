using NextHome.Domain.Entities;

namespace NextHome.Application.Interfaces.Properties;

public interface IGetAllPropertiesUseCase
{
    Task<IEnumerable<Property>> ExecuteAsync();
}

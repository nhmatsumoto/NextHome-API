using NextHome.Domain.Entities;

namespace NextHome.Application.Interfaces.Properties;

public interface IUpdatePropertyUseCase
{
    Task<bool> ExecuteAsync(Property property);
}

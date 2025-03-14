using NextHome.Domain.Entities;

namespace NextHome.Application.Interfaces.Properties;

public interface ICreatePropertyUseCase
{
    Task<int> ExecuteAsync(Property property);
}

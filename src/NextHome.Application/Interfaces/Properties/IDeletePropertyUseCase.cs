namespace NextHome.Application.Interfaces.Properties;

public interface IDeletePropertyUseCase
{
    Task<bool> ExecuteAsync(int id);
}

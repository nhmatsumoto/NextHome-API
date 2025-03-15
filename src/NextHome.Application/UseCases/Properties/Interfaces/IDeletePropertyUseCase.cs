namespace NextHome.Application.UseCases.Properties.Interfaces;

public interface IDeletePropertyUseCase
{
    Task<bool> ExecuteAsync(int id, CancellationToken cancellationToken);
}

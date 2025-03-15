namespace NextHome.Application.UseCases.Users.Interfaces;

public interface IDeleteUserUseCase
{
    Task<bool> Execute(int id, CancellationToken cancellationToken);
}

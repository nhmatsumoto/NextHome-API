using NextHome.Domain.Entities;

namespace NextHome.Application.UseCases.Users.Interfaces;

public interface IGetAllUsersUseCase
{
    Task<IEnumerable<User>> Execute(CancellationToken cancellationToken);
}

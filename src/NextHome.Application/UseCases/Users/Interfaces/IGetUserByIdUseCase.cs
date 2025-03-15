using NextHome.Domain.Entities;

namespace NextHome.Application.UseCases.Users.Interfaces;

public interface IGetUserByIdUseCase
{
    Task<User?> Execute(int id, CancellationToken cancellationToken);
}

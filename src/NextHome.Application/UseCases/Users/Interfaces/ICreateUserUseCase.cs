using NextHome.Application.DTOs;

namespace NextHome.Application.UseCases.Users.Interfaces;

public interface ICreateUserUseCase
{
    Task<int> Execute(CreateUserDto dto, CancellationToken cancellationToken);
}

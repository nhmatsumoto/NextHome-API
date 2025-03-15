using NextHome.Application.DTOs;

namespace NextHome.Application.UseCases.Users.Interfaces;

public interface IUpdateUserUseCase
{
    Task<bool> Execute(UpdateUserDto dto, CancellationToken cancellationToken);
}

using NextHome.Application.UseCases.Users.Interfaces;
using NextHome.Domain.Entities;
using NextHome.Domain.Interfaces;

namespace NextHome.Application.UseCases.Users;

public class GetUserByIdUseCase : IGetUserByIdUseCase
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> Execute(int id, CancellationToken cancellationToken = default)
    {
        return await _userRepository.GetByIdAsync(id, cancellationToken);
    }
}

using NextHome.Application.UseCases.Users.Interfaces;
using NextHome.Domain.Entities;
using NextHome.Domain.Interfaces;
using System.Threading;

namespace NextHome.Application.UseCases.Users;

public class GetAllUsersUseCase : IGetAllUsersUseCase
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<User>> Execute(CancellationToken cancellationToken)
    {
        return await _userRepository.GetAllAsync(cancellationToken);
    }
}

using NextHome.Application.UseCases.Users.Interfaces;
using NextHome.Domain.Interfaces;

namespace NextHome.Application.UseCases.Users;

public class DeleteUserUseCase : IDeleteUserUseCase
{
    private readonly IUserRepository _userRepository;

    public DeleteUserUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }


    // Mudar para não deletar efetivamente o usuário (apenas atualizar para desabilitado)
    public async Task<bool> Execute(int id, CancellationToken cancellationToken = default)
    {
        var userExists = await _userRepository.GetByIdAsync(id, cancellationToken);
        if (userExists == null) throw new KeyNotFoundException("Usuário não encontrado.");

        return await _userRepository.DeleteAsync(id, cancellationToken);
    }
}

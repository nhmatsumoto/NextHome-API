using FluentValidation;
using NextHome.Application.DTOs;
using NextHome.Application.UseCases.Users.Interfaces;
using NextHome.Domain.Entities;
using NextHome.Domain.Interfaces;

namespace NextHome.Application.UseCases.Users;

public class UpdateUserUseCase : IUpdateUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<User> _validator;

    public UpdateUserUseCase(IUserRepository userRepository, IValidator<User> validator)
    {
        _userRepository = userRepository;
        _validator = validator;
    }

    public async Task<bool> Execute(UpdateUserDto dto, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(dto.Id, cancellationToken);
        if (user == null) throw new KeyNotFoundException("Usuário não encontrado.");

        user.Username = dto.Username;
        user.Email = dto.Email;

        var validationResult = await _validator.ValidateAsync(user);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors.First().ErrorMessage);

        return await _userRepository.UpdateAsync(user, cancellationToken);
    }
}
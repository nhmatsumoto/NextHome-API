using FluentValidation;
using NextHome.Application.DTOs;
using NextHome.Application.UseCases.Users.Interfaces;
using NextHome.Domain.Entities;
using NextHome.Domain.Interfaces;

namespace NextHome.Application.UseCases.Users;

public class CreateUserUseCase : ICreateUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<User> _validator;

    public CreateUserUseCase(IUserRepository userRepository, IValidator<User> validator)
    {
        _userRepository = userRepository;
        _validator = validator;
    }

    public async Task<int> Execute(CreateUserDto dto, CancellationToken cancellationToken = default)
    {
        if (await _userRepository.ExistsByEmail(dto.Email, cancellationToken))
            throw new ValidationException("E-mail já cadastrado.");

        var user = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            IsAvailable = true,
            
        };

        var validationResult = await _validator.ValidateAsync(user);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors.First().ErrorMessage);

        return await _userRepository.AddAsync(user, cancellationToken);
    }
}

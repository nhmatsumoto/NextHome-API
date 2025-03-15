using FluentValidation;
using NextHome.Domain.Entities;

namespace NextHome.Application.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(u => u.Username).NotEmpty().WithMessage("Nome é obrigatório.");
        RuleFor(u => u.Email).NotEmpty().EmailAddress().WithMessage("E-mail inválido.");
    }
}

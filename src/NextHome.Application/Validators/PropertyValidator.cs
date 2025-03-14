using FluentValidation;
using NextHome.Domain.Entities;

namespace NextHome.Application.Validators;

public class PropertyValidator : AbstractValidator<Property>
{
    public PropertyValidator()
    {
        RuleFor(p => p.Title)
            .NotEmpty().WithMessage("O título do imóvel é obrigatório.")
            .MaximumLength(100).WithMessage("O título não pode ter mais de 100 caracteres.");

        RuleFor(p => p.Description)
            .NotEmpty().WithMessage("A descrição do imóvel é obrigatória.")
            .MaximumLength(1000).WithMessage("A descrição não pode ter mais de 1000 caracteres.");

        RuleFor(p => p.Price)
            .GreaterThan(0).WithMessage("O preço do imóvel deve ser maior que zero.");

        RuleFor(p => p.ManagementFee)
            .GreaterThanOrEqualTo(0).WithMessage("A taxa de administração não pode ser negativa.");

        RuleFor(p => p.DepositShikikin)
            .GreaterThanOrEqualTo(0).WithMessage("O depósito não pode ser negativo.");

        RuleFor(p => p.KeyMoneyReikin)
            .GreaterThanOrEqualTo(0).WithMessage("As luvas não podem ser negativas.");

        RuleFor(p => p.Bedrooms)
            .GreaterThanOrEqualTo(0).WithMessage("O número de quartos não pode ser negativo.");

        RuleFor(p => p.Bathrooms)
            .GreaterThanOrEqualTo(0).WithMessage("O número de banheiros não pode ser negativo.");

        RuleFor(p => p.ParkingSpaces)
            .GreaterThanOrEqualTo(0).WithMessage("O número de vagas de estacionamento não pode ser negativo.");

        RuleFor(p => p.FloorArea)
            .GreaterThan(0).WithMessage("A área total deve ser maior que zero.");

        RuleFor(p => p.YearBuilt)
            .InclusiveBetween(1800, DateTime.Now.Year).WithMessage($"O ano de construção deve estar entre 1800 e {DateTime.Now.Year}.");

        RuleFor(p => p.Type)
            .IsInEnum().WithMessage("Tipo de imóvel inválido.");

        RuleFor(p => p.Category)
            .IsInEnum().WithMessage("Categoria de anúncio inválida.");

        RuleFor(p => p.AddressId)
            .GreaterThan(0).WithMessage("O endereço do imóvel é obrigatório.");

        RuleFor(p => p.RealEstateAgencyId)
            .GreaterThan(0).WithMessage("A imobiliária associada é obrigatória.");
    }
}

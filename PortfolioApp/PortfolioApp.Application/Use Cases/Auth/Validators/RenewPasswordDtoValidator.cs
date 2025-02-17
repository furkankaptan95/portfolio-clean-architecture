using FluentValidation;
using PortfolioApp.Core.DTOs.Auth;

namespace PortfolioApp.Application.Use_Cases.Auth.Validators;
public class RenewPasswordDtoValidator : AbstractValidator<RenewPasswordDto>
{
    public RenewPasswordDtoValidator()
    {
        RuleFor(x => x.Email)
            .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Email alanı boş bırakılamaz.")
            .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.")
            .MaximumLength(100).WithMessage("Email 100 karakterden uzun olamaz.");

        RuleFor(x => x.Token)
            .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Token alanı boş bırakılamaz.");
    }
}

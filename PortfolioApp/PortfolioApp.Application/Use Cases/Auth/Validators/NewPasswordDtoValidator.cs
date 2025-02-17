using FluentValidation;
using PortfolioApp.Core.DTOs.Auth;

namespace PortfolioApp.Application.Use_Cases.Auth.Validators;
public class NewPasswordDtoValidator : AbstractValidator<NewPasswordDto>
{
    public NewPasswordDtoValidator()
    {
        RuleFor(x => x.Email)
            .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Email alanı boş bırakılamaz.")
            .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.")
            .MaximumLength(100).WithMessage("Email 100 karakterden uzun olamaz.");

        RuleFor(x => x.Token)
            .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Token alanı boş bırakılamaz.");

        RuleFor(x => x.Password)
            .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Şifre alanı boş bırakılamaz.")
            .MinimumLength(8).WithMessage("Şifre en az 8 karakter olmalıdır.")
            .MaximumLength(15).WithMessage("Şifre en fazla 15 karakter olabilir.");
    }
}

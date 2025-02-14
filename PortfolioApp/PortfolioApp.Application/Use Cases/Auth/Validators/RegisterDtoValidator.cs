using FluentValidation;
using PortfolioApp.Core.DTOs.Auth;

namespace PortfolioApp.Application.Use_Cases.Auth.Validators;
public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.Email)
          .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Email alanı boş bırakılamaz.")
          .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.")
          .MaximumLength(100).WithMessage("Email 100 karakterden uzun olamaz.");

        RuleFor(x => x.Username)
          .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Kullanıcı adı alanı boş bırakılamaz.")
          .MaximumLength(50).WithMessage("Kullanıcı adı 20 karakterden uzun olamaz.")
          .Matches(@"^\S*$").WithMessage("Kullanıcı adı boşluk içeremez.");

        RuleFor(x => x.Password)
            .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Şifre alanı boş bırakılamaz.")
            .MinimumLength(8).WithMessage("Şifre en az 8 karakter olmalıdır.")
            .MaximumLength(15).WithMessage("Şifre en fazla 15 karakter olabilir.");

        RuleFor(x => x.Firstname)
         .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("İsim kısmı boş olamaz.")
         .MaximumLength(50).WithMessage("İsim maksimum 50 karakter olabilir.");

        RuleFor(x => x.Lastname)
           .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Soyisim kısmı boş olamaz.")
           .MaximumLength(50).WithMessage("Soyisim maksimum 50 karakter olabilir.");


    }
}
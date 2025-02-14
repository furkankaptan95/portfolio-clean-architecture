using FluentValidation;
using PortfolioApp.Core.DTOs.Web.ContactMessage;

namespace PortfolioApp.Application.Use_Cases.ContactMessage.Validators;
public class AddContactMessageDtoValidator : AbstractValidator<AddContactMessageDto>
{
    public AddContactMessageDtoValidator()
    {
        RuleFor(x => x.Name)
            .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("İsim kısmı boş olamaz.")
            .MaximumLength(50).WithMessage("İsim kısmı en fazla 50 karakter olabilir.");

        RuleFor(x => x.Email)
            .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Email kısmı zorunludur.")
            .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.")
            .MaximumLength(100).WithMessage("Email kısmı en fazla 100 karakter olabilir.");

        RuleFor(x => x.Subject)
             .Must(subject => subject == null || !string.IsNullOrWhiteSpace(subject))
            .WithMessage("Konu kısmı yalnızca boşluklardan oluşamaz.")
            .MaximumLength(100).WithMessage("Konu kısmı en fazla 100 karakter olabilir.");

        RuleFor(x => x.Message)
            .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Mesaj kısmı boş olamaz.");
    }
}


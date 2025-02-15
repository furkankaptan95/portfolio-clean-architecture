using FluentValidation;
using PortfolioApp.Core.DTOs.Admin.ContactMessage;

namespace PortfolioApp.Application.Use_Cases.ContactMessage.Validators;
public class ReplyContactMessageDtoValidator : AbstractValidator<ReplyContactMessageDto>
{
    public ReplyContactMessageDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id 0'dan büyük olmalıdır.");

        RuleFor(x => x.ReplyMessage)
            .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Mesaj kısmı boş olamaz.");
    }
}

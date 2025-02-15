using FluentValidation;
using PortfolioApp.Core.DTOs.Admin.Project;

namespace PortfolioApp.Application.Use_Cases.Project.Validators;
public class UpdateProjectApiDtoValidator : AbstractValidator<UpdateProjectApiDto>
{
    public UpdateProjectApiDtoValidator()
    {
        RuleFor(x => x.Id)           
             .GreaterThan(0).WithMessage("Id 0'dan büyük olmalıdır.");

        RuleFor(x => x.Title)
            .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Başlık kısmı boş olamaz.")
            .MaximumLength(100).WithMessage("Başlık en fazla 100 karakter olabilir.");

        RuleFor(x => x.Description)
           .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Açıklama kısmı boş olamaz.");

        RuleFor(x => x.ImageUrl)
            .Must(name => name == null || !string.IsNullOrWhiteSpace(name))
          .WithMessage("ImageUrl kısmı sadece boşluk olamaz.");
    }
}
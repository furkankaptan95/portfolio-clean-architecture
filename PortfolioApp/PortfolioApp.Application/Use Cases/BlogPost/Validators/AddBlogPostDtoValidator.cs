using FluentValidation;
using PortfolioApp.Core.DTOs.Admin.BlogPost;

namespace PortfolioApp.Application.Use_Cases.BlogPost.Validators;

public class AddBlogPostDtoValidator : AbstractValidator<AddBlogPostDto>
{
    public AddBlogPostDtoValidator()
    {
        RuleFor(x => x.Title)
           .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Başlık kısmı boş olamaz.")
            .MaximumLength(100).WithMessage("Başlık maksimum 100 karakter olabilir.");

        RuleFor(x => x.Content)
           .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("İçerik kısmı boş olamaz.");

    }
}

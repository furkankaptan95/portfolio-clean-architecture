using FluentValidation;
using PortfolioApp.Core.DTOs.Admin.AboutMe;

namespace PortfolioApp.Application.Use_Cases.AboutMe.Validators;
public class UpdateAboutMeApiDtoValidator : AbstractValidator<UpdateAboutMeApiDto>
{
    public UpdateAboutMeApiDtoValidator()
    {
        RuleFor(x => x.AboutMeImageUrl)
          .Must(name => name == null || !string.IsNullOrWhiteSpace(name))
          .WithMessage("AboutMeImageUrl kısmı sadece boşluk olamaz.").MaximumLength(255).WithMessage("AboutMeImageUrl maksimum 255 karakter olabilir.");

        RuleFor(x => x.HeroImageUrl)
          .Must(name => name == null || !string.IsNullOrWhiteSpace(name))
          .WithMessage("HeroImageUrl kısmı sadece boşluk olamaz.").MaximumLength(255).WithMessage("HeroImageUrl maksimum 255 karakter olabilir.");

        RuleFor(x => x.LinkedInUrl)
             .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("LinkedInUrl kısmı boş olamaz.")
             .MaximumLength(255).WithMessage("LinkedInUrl maksimum 255 karakter olabilir.");

        RuleFor(x => x.GithubUrl)
             .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("GithubUrl kısmı boş olamaz.")
             .MaximumLength(255).WithMessage("GithubUrl maksimum 255 karakter olabilir.");

        RuleFor(x => x.TwitterUrl)
         .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("TwitterUrl kısmı boş olamaz.")
         .MaximumLength(255).WithMessage("TwitterUrl maksimum 255 karakter olabilir.");

        RuleFor(x => x.InstagramUrl)
           .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("InstagramUrl kısmı boş olamaz.")
           .MaximumLength(255).WithMessage("InstagramUrl maksimum 255 karakter olabilir.");

        RuleFor(x => x.MediumUrl)
           .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("MediumUrl kısmı boş olamaz.")
           .MaximumLength(255).WithMessage("MediumUrl maksimum 255 karakter olabilir.");

        RuleFor(x => x.CvUrl)
              .Must(name => name == null || !string.IsNullOrWhiteSpace(name))
              .WithMessage("CvUrl kısmı sadece boşluk olamaz.").MaximumLength(255).WithMessage("CvUrl maksimum 255 karakter olabilir.");

        RuleFor(x => x.Introduction)
            .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Giriş kısmı boş olamaz.")
            .MaximumLength(100).WithMessage("Giriş maksimum 100 karakter olabilir.");

        RuleFor(x => x.Email)
          .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Email kısmı boş olamaz.")
          .MaximumLength(100).WithMessage("Email maksimum 100 karakter olabilir.")
          .EmailAddress().WithMessage("Geçerli bir e-posta adresi girin.");

        RuleFor(x => x.FullName)
            .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Tam isim kısmı boş olamaz.")
            .MaximumLength(50).WithMessage("Tam isim maksimum 50 karakter olabilir.");

        RuleFor(x => x.Field)
            .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Alan kısmı boş olamaz.")
            .MaximumLength(50).WithMessage("Alan maksimum 50 karakter olabilir.");
    }
}


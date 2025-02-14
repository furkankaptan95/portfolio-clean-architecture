﻿using FluentValidation;
using PortfolioApp.Core.DTOs.Admin.BlogPost;

namespace PortfolioApp.Application.Use_Cases.BlogPost.Validators;
public class UpdateBlogPostDtoValidator : AbstractValidator<UpdateBlogPostDto>
{
    public UpdateBlogPostDtoValidator()
    {
        RuleFor(x => x.Id)
             .GreaterThan(0).WithMessage("Id 0'dan büyük olmalıdır.");

        RuleFor(x => x.Title)
            .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Başlık kısmı boş olamaz.")
            .MaximumLength(100).WithMessage("Başlık maksimum 100 karakter olabilir.");

        RuleFor(x => x.Content)
         .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("İçerik kısmı boş olamaz.");
    }
}
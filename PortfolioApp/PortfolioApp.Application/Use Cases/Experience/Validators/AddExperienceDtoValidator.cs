﻿using FluentValidation;
using PortfolioApp.Core.DTOs.Admin.Experience;

namespace PortfolioApp.Application.Use_Cases.Experience.Validators;
public class AddExperienceDtoValidator : AbstractValidator<AddExperienceDto>
{
    public AddExperienceDtoValidator()
    {
        RuleFor(x => x.Title)
            .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Başlık kısmı boş olamaz.")
            .MaximumLength(100).WithMessage("Başlık maksimum 100 karakter olabilir.");

        RuleFor(x => x.Company)
           .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Şirket kısmı boş olamaz.")
           .MaximumLength(100).WithMessage("Şirket maksimum 100 karakter olabilir.");

        RuleFor(x => x.Description)
           .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Açıklama kısmı boş olamaz.");

        RuleFor(x => x.StartDate)
          .NotEmpty().WithMessage("Başlangıç tarihi gerekli.")  // Boş olamaz
          .Must(BeAValidStartDate).WithMessage("Geçerli bir tarih olmalı.");

        RuleFor(x => x.EndDate)
                .Must(BeAValidEndDate).When(x => x.EndDate.HasValue).WithMessage("Geçerli bir tarih olmalı.") // Değer girildiyse kontrol et
                .Must((viewModel, endDate) => !endDate.HasValue || endDate > viewModel.StartDate)
                .WithMessage("Bitiş tarihi, başlangıç tarihinden sonra olmalı.");

    }
    private bool BeAValidEndDate(DateTime? date)
    {
        return date.Value != default;
    }

    private bool BeAValidStartDate(DateTime date)
    {
        return date != default;
    }

}

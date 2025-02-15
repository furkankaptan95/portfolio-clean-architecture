using FluentValidation;
using PortfolioApp.Core.DTOs.Admin.Education;

namespace PortfolioApp.Application.Use_Cases.Education.Validators;
public class AddEducationDtoValidator : AbstractValidator<AddEducationDto>
{
    public AddEducationDtoValidator()
    {
        RuleFor(x => x.Degree)
           .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Derece kısmı boş olamaz.")
           .MaximumLength(50).WithMessage("Derece maksimum 50 karakter olabilir.");

        RuleFor(x => x.School)
           .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Okul kısmı boş olamaz.")
           .MaximumLength(100).WithMessage("Okul maksimum 100 karakter olabilir.");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Başlangıç tarihi gerekli.")
            .Must(BeAValidStartDate).WithMessage("Geçerli bir tarih olmalı.")
            .Must(date => date < DateTime.Now).WithMessage("Başlangıç tarihi şu anki tarihten önce olmalı."); // Yeni kural eklendi

        RuleFor(x => x.EndDate)
                .Must(BeAValidEndDate).When(x => x.EndDate.HasValue).WithMessage("Geçerli bir tarih olmalı.")
                .Must((viewModel, endDate) => !endDate.HasValue || endDate > viewModel.StartDate)
                .WithMessage("Bitiş tarihi, başlangıç tarihinden sonra olmalı.")
                .Must(date => !date.HasValue || date < DateTime.Now).WithMessage("Bitiş tarihi şu anki tarihten önce olmalı."); // Yeni kural eklendi
    }

    private bool BeAValidEndDate(DateTime? date)
    {
        return date.HasValue && date.Value != default;
    }

    private bool BeAValidStartDate(DateTime date)
    {
        return date != default;
    }
}


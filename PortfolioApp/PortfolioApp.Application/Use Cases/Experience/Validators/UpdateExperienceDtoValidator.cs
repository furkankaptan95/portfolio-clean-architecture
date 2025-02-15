using FluentValidation;
using PortfolioApp.Core.DTOs.Admin.Experience;

namespace PortfolioApp.Application.Use_Cases.Experience.Validators;
public class UpdateExperienceDtoValidator : AbstractValidator<UpdateExperienceDto>
{
    public UpdateExperienceDtoValidator()
    {
        RuleFor(x => x.Id)
             .GreaterThan(0).WithMessage("Id 0'dan büyük olmalıdır.");

        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id bilgisi boş olamaz.")
             .GreaterThan(0).WithMessage("Id 0'dan büyük olmalıdır.");

        RuleFor(x => x.Title)
           .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Başlık kısmı boş olamaz.")
           .MaximumLength(100).WithMessage("Başlık maksimum 100 karakter olabilir.");

        RuleFor(x => x.Company)
           .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Şirket kısmı boş olamaz.")
           .MaximumLength(100).WithMessage("Şirket maksimum 100 karakter olabilir.");

        RuleFor(x => x.Description)
           .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Açıklama kısmı boş olamaz.");

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

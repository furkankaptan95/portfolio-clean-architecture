﻿using FluentValidation;
using PortfolioApp.Core.DTOs.Admin.PersonalInfo;

namespace PortfolioApp.Application.Use_Cases.PersonalInfo.Validators;
public class UpdatePersonalInfoDtoValidator : AbstractValidator<UpdatePersonalInfoDto>
{
    public UpdatePersonalInfoDtoValidator()
    {
        RuleFor(x => x.About)
         .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Hakkımda kısmı boş olamaz.")
         .MaximumLength(300).WithMessage("Hakkımda kısmı maksimum 300 karakter olabilir.");

        RuleFor(x => x.Name)
           .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("İsim kısmı boş olamaz.")
           .MaximumLength(50).WithMessage("İsim maksimum 50 karakter olabilir.");

        RuleFor(x => x.Surname)
           .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Soyisim kısmı boş olamaz.")
           .MaximumLength(50).WithMessage("Soyisim maksimum 50 karakter olabilir.");

        RuleFor(x => x.Adress)
          .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Adres kısmı boş olamaz.")
          .MaximumLength(50).WithMessage("Adres maksimum 50 karakter olabilir.");

        RuleFor(x => x.BirthDate)
           .NotEmpty().WithMessage("Doğum tarihi gerekli.")
           .Must(BeAValidDate).WithMessage("Geçerli bir tarih giriniz.")
           .Must(date => date < DateTime.Now).WithMessage("Doğum tarihi şu anki tarihten önce olmalı.");
    }
    private bool BeAValidDate(DateTime date)
    {
        return date != default;
    }
}


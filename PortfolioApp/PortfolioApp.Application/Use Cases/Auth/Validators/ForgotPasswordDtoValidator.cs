﻿using FluentValidation;
using PortfolioApp.Core.DTOs.Auth;

namespace PortfolioApp.Application.Use_Cases.Auth.Validators;
public class ForgotPasswordDtoValidator : AbstractValidator<ForgotPasswordDto>
{
    public ForgotPasswordDtoValidator()
    {
        RuleFor(x => x.Email)
            .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Email alanı boş bırakılamaz.")
            .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.")
            .MaximumLength(100).WithMessage("Email 100 karakterden uzun olamaz.");
    }
}


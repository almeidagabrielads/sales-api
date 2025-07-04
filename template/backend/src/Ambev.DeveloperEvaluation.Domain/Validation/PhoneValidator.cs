// <copyright file="PhoneValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ambev.DeveloperEvaluation.Domain.Validation;

using FluentValidation;

public class PhoneValidator : AbstractValidator<string>
{
    public PhoneValidator()
    {
        this.RuleFor(phone => phone)
            .NotEmpty().WithMessage("The phone cannot be empty.")
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("The phone format is not valid.");
    }
}

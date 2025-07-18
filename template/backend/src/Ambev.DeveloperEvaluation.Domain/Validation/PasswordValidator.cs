﻿// <copyright file="PasswordValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ambev.DeveloperEvaluation.Domain.Validation;

using FluentValidation;

public class PasswordValidator : AbstractValidator<string>
{
    public PasswordValidator()
    {
        this.RuleFor(password => password)
            .NotEmpty()
            .MinimumLength(8)
            .Matches(@"[A-Z]+").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"[0-9]+").WithMessage("Password must contain at least one number.")
            .Matches(@"[\!\?\*\.\@\#\$\%\^\&\+\=]+").WithMessage("Password must contain at least one special character.");
    }
}
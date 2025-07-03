// <copyright file="UserValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ambev.DeveloperEvaluation.Domain.Validation;

using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

using FluentValidation;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        this.RuleFor(user => user.Email).SetValidator(new EmailValidator());

        this.RuleFor(user => user.Username)
            .NotEmpty()
            .MinimumLength(3).WithMessage("Username must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("Username cannot be longer than 50 characters.");

        this.RuleFor(user => user.Password).SetValidator(new PasswordValidator());

        this.RuleFor(user => user.Phone)
            .Matches(@"^\+[1-9]\d{10,14}$")
            .WithMessage("Phone number must start with '+' followed by 11-15 digits.");

        this.RuleFor(user => user.Status)
            .NotEqual(UserStatus.Unknown)
            .WithMessage("User status cannot be Unknown.");

        this.RuleFor(user => user.Role)
            .NotEqual(UserRole.None)
            .WithMessage("User role cannot be None.");
    }
}

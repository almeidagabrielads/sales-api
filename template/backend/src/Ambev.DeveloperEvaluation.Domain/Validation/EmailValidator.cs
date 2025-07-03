// <copyright file="EmailValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ambev.DeveloperEvaluation.Domain.Validation;

using System.Text.RegularExpressions;

using FluentValidation;

public class EmailValidator : AbstractValidator<string>
{
    public EmailValidator()
    {
        this.RuleFor(email => email)
            .NotEmpty()
            .WithMessage("The email address cannot be empty.")
            .MaximumLength(100)
            .WithMessage("The email address cannot be longer than 100 characters.")
            .Must(this.BeValidEmail)
            .WithMessage("The provided email address is not valid.");
    }

    private bool BeValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return false;
        }

        // More strict email validation
        Regex regex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
        return regex.IsMatch(email);
    }
}

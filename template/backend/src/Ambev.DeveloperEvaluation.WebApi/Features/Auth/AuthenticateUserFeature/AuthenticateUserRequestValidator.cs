// <copyright file="AuthenticateUserRequestValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ambev.DeveloperEvaluation.WebApi.Features.Auth.AuthenticateUserFeature;

using FluentValidation;

/// <summary>
/// Validator for AuthenticateUserRequest.
/// </summary>
public class AuthenticateUserRequestValidator : AbstractValidator<AuthenticateUserRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticateUserRequestValidator"/> class.
    /// Initializes validation rules for AuthenticateUserRequest.
    /// </summary>
    public AuthenticateUserRequestValidator()
    {
        this.RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Invalid email format");

        this.RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required");
    }
}

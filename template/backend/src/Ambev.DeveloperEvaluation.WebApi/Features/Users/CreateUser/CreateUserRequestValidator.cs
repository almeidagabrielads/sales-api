// <copyright file="CreateUserRequestValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;

using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;

using FluentValidation;

/// <summary>
/// Validator for CreateUserRequest that defines validation rules for user creation.
/// </summary>
public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateUserRequestValidator"/> class.
    /// Initializes a new instance of the CreateUserRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Email: Must be valid format (using EmailValidator)
    /// - Username: Required, length between 3 and 50 characters
    /// - Password: Must meet security requirements (using PasswordValidator)
    /// - Phone: Must match international format (+X XXXXXXXXXX)
    /// - Status: Cannot be Unknown
    /// - Role: Cannot be None.
    /// </remarks>
    public CreateUserRequestValidator()
    {
        this.RuleFor(user => user.Email).SetValidator(new EmailValidator());
        this.RuleFor(user => user.Username).NotEmpty().Length(3, 50);
        this.RuleFor(user => user.Password).SetValidator(new PasswordValidator());
        this.RuleFor(user => user.Phone).Matches(@"^\+?[1-9]\d{1,14}$");
        this.RuleFor(user => user.Status).NotEqual(UserStatus.Unknown);
        this.RuleFor(user => user.Role).NotEqual(UserRole.None);
    }
}
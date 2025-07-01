// <copyright file="CreateUserValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser;

using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;

using FluentValidation;

/// <summary>
/// Validator for CreateUserCommand that defines validation rules for user creation command.
/// </summary>
public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateUserCommandValidator"/> class.
    /// Initializes a new instance of the CreateUserCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Email: Must be in valid format (using EmailValidator)
    /// - Username: Required, must be between 3 and 50 characters
    /// - Password: Must meet security requirements (using PasswordValidator)
    /// - Phone: Must match international format (+X XXXXXXXXXX)
    /// - Status: Cannot be set to Unknown
    /// - Role: Cannot be set to None.
    /// </remarks>
    public CreateUserCommandValidator()
    {
        this.RuleFor(user => user.Email).SetValidator(new EmailValidator());
        this.RuleFor(user => user.Username).NotEmpty().Length(3, 50);
        this.RuleFor(user => user.Password).SetValidator(new PasswordValidator());
        this.RuleFor(user => user.Phone).Matches(@"^\+?[1-9]\d{1,14}$");
        this.RuleFor(user => user.Status).NotEqual(UserStatus.Unknown);
        this.RuleFor(user => user.Role).NotEqual(UserRole.None);
    }
}
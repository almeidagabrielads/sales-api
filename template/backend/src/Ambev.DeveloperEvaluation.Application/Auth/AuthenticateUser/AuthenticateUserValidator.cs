// <copyright file="AuthenticateUserValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser
{
    using FluentValidation;

    public class AuthenticateUserValidator : AbstractValidator<AuthenticateUserCommand>
    {
        public AuthenticateUserValidator()
        {
            this.RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            this.RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6);
        }
    }
}

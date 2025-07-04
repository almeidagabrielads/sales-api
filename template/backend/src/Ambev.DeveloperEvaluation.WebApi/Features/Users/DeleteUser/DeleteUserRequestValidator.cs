// <copyright file="DeleteUserRequestValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.DeleteUser;

using FluentValidation;

/// <summary>
/// Validator for DeleteUserRequest.
/// </summary>
public class DeleteUserRequestValidator : AbstractValidator<DeleteUserRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteUserRequestValidator"/> class.
    /// Initializes validation rules for DeleteUserRequest.
    /// </summary>
    public DeleteUserRequestValidator()
    {
        this.RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("User ID is required");
    }
}

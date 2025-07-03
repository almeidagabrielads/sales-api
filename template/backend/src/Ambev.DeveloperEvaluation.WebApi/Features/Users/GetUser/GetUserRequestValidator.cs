// <copyright file="GetUserRequestValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;

using FluentValidation;

/// <summary>
/// Validator for GetUserRequest.
/// </summary>
public class GetUserRequestValidator : AbstractValidator<GetUserRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetUserRequestValidator"/> class.
    /// Initializes validation rules for GetUserRequest.
    /// </summary>
    public GetUserRequestValidator()
    {
        this.RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("User ID is required");
    }
}

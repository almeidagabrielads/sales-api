// <copyright file="CustomerValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ambev.DeveloperEvaluation.Application.Validators;

using Ambev.DeveloperEvaluation.Domain.Entities;

using FluentValidation;

public class CustomerValidator : AbstractValidator<Customer>
{
    public CustomerValidator()
    {
        this.RuleFor(customer => customer.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("Customer Id must be valid.");

        this.RuleFor(customer => customer.ExternalId)
            .NotEmpty()
            .WithMessage("Customer ExternalId is required.")
            .MaximumLength(50)
            .WithMessage("Customer ExternalId must not exceed 50 characters.");

        this.RuleFor(customer => customer.Name)
            .NotEmpty()
            .WithMessage("Customer name must not be empty.")
            .MaximumLength(500)
            .WithMessage("Customer name must not exceed 500 characters.");
    }
}

// <copyright file="SaleValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ambev.DeveloperEvaluation.Domain.Validation;

using Ambev.DeveloperEvaluation.Application.Validators;
using Ambev.DeveloperEvaluation.Domain.Entities;

using FluentValidation;

public class SaleValidator : AbstractValidator<Sale>
{
    public SaleValidator()
    {
        this.RuleFor(sale => sale.SaleNumber)
            .NotEmpty()
            .WithMessage("Sale number must not be empty.")
            .MaximumLength(50)
            .WithMessage("Sale number must not exceed 50 characters.");

        this.RuleFor(sale => sale.Customer)
            .SetValidator(new CustomerValidator());

        this.RuleFor(sale => sale.Branch)
            .SetValidator(new BranchValidator());

        this.RuleFor(sale => sale.Items)
            .NotEmpty()
            .WithMessage("At least one sale item is required.");

        this.RuleForEach(sale => sale.Items)
            .SetValidator(new SaleItemValidator());
    }
}
// <copyright file="SaleItemValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ambev.DeveloperEvaluation.Application.Validators;

using Ambev.DeveloperEvaluation.Domain.Entities;

using FluentValidation;

public class SaleItemValidator : AbstractValidator<SaleItem>
{
    public SaleItemValidator()
    {
        this.RuleFor(saleItem => saleItem.Product)
            .SetValidator(new ProductValidator());

        this.RuleFor(i => i.Quantity)
            .InclusiveBetween(1, 20)
            .WithMessage("Quantity must be between 1 and 20.");

        this.RuleFor(i => i.UnitPrice)
            .GreaterThan(0)
            .WithMessage("Unit price must be greater than zero.");
    }
}

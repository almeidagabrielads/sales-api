// <copyright file="ProductValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ambev.DeveloperEvaluation.Application.Validators;

using Ambev.DeveloperEvaluation.Domain.Entities;

using FluentValidation;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        this.RuleFor(product => product.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("Product Id must be valid.");

        this.RuleFor(product => product.ExternalId)
            .NotEmpty()
            .WithMessage("Product ExternalId is required.");

        this.RuleFor(product => product.Name)
            .NotEmpty()
            .WithMessage("Product name must not be empty.")
            .MaximumLength(500)
            .WithMessage("Product name must not exceed 500 characters.");

        this.RuleFor(product => product.UnitPrice)
            .GreaterThan(0)
            .WithMessage("Product unit price must be greater than 0.");
    }
}

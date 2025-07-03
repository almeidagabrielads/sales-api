// <copyright file="SaleValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ambev.DeveloperEvaluation.Domain.Validation;
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
        
        this.RuleFor(sale => sale.CreatedAt)
            .NotEmpty()
            .WithMessage("Sale date must not be empty.")
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Sale date cannot be in the future.");
        
        this.RuleFor(sale => sale.CustomerId)
            .NotEmpty()
            .WithMessage("Customer ID must not be empty.");
        
        this.RuleFor(sale => sale.CustomerName)
            .NotEmpty()
            .WithMessage("Customer name must not be empty.")
            .MaximumLength(100)
            .WithMessage("Customer name must not exceed 100 characters.");
        
        this.RuleFor(sale => sale.BranchId)
            .NotEmpty()
            .WithMessage("Branch ID must not be empty.");
        
        this.RuleFor(sale => sale.BranchName)
            .NotEmpty()
            .WithMessage("Branch name must not be empty.")
            .MaximumLength(100)
            .WithMessage("Branch name must not exceed 100 characters.");

        this.RuleFor(sale => sale.Items)
            .NotEmpty()
            .WithMessage("At least one sale item is required.");

        this.RuleForEach(sale => sale.Items)
            .SetValidator(new SaleItemValidator());
    }
}
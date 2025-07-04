namespace Ambev.DeveloperEvaluation.Domain.Validation;

using Ambev.DeveloperEvaluation.Domain.Entities;

using FluentValidation;

public class SaleItemValidator : AbstractValidator<SaleItem>
{
    public SaleItemValidator()
    {
        this.RuleFor(saleItem => saleItem.ProductId)
            .NotEmpty()
            .WithMessage("Product ID cannot be empty.");
        
        this.RuleFor(saleItem => saleItem.ProductName)
            .NotEmpty()
            .WithMessage("Product name cannot be empty.")
            .MaximumLength(100)
            .WithMessage("Product name cannot exceed 100 characters.");
        
        this.RuleFor(i => i.Quantity)
            .InclusiveBetween(1, 20)
            .WithMessage("Quantity must be between 1 and 20.");
        
        this.RuleFor(i => i.UnitPrice)
            .GreaterThan(0)
            .WithMessage("Unit price must be greater than zero.");
        
        this.RuleFor(i => i.SaleId)
            .NotEmpty()
            .WithMessage("Sale ID cannot be empty.");
    }
}

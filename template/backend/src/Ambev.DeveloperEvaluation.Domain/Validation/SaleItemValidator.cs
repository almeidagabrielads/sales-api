namespace Ambev.DeveloperEvaluation.Domain.Validation;

using Ambev.DeveloperEvaluation.Domain.Entities;

using FluentValidation;

public class SaleItemValidator : AbstractValidator<SaleItem>
{
    public SaleItemValidator()
    {
        this.RuleFor(saleItem => saleItem.ProductExternalId)
            .NotEmpty()
            .WithMessage("Product ID cannot be empty.");
        
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

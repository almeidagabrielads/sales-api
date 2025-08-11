namespace Ambev.DeveloperEvaluation.Domain.Validation;

using Entities;
using FluentValidation;

public class SaleValidator: AbstractValidator<Sale>
{
    public SaleValidator()
    {
        this.RuleFor(sale => sale.Id)
            .NotEmpty()
            .WithMessage("Sale Id must not be empty.");
        
        this.RuleFor(sale => sale.SaleNumber)
            .NotEmpty()
            .WithMessage("Sale Number must not be empty.")
            .MaximumLength(50)
            .WithMessage("Sale Number must not exceed 50 characters.");
        
        this.RuleFor(sale => sale.CreatedAt)
            .NotEmpty()
            .WithMessage("Sale Created At must not be empty.")
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Sale Created At cannot be in the future.");
        
        this.RuleFor(sale => sale.CustomerExternalId)
            .NotEmpty()
            .WithMessage("Customer External Id must not be empty.");
        
        this.RuleFor(sale => sale.BranchExternalId)
            .NotEmpty()
            .WithMessage("Branch External Id must not be empty.");
        
        this.RuleFor(sale => sale.Items)
            .NotEmpty()
            .WithMessage("At least one sale item is required.");

        this.RuleForEach(sale => sale.Items)
            .SetValidator(new SaleItemValidator());
    }
}
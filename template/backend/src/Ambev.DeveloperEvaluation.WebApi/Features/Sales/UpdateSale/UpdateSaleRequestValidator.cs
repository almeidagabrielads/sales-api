using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
{
    public UpdateSaleRequestValidator()
    {
        this.RuleFor(sale => sale.Id)
            .NotEmpty()
            .WithMessage("Sale Id must not be empty.");
        
        this.RuleFor(sale => sale.NewSaleNumber)
            .NotEmpty()
            .WithMessage("Sale number must not be empty.")
            .MaximumLength(50)
            .WithMessage("Sale number must not exceed 50 characters.");
        
        this.RuleFor(sale => sale.NewCustomerId)
            .NotNull()
            .WithMessage("Customer Id must not be null.")
            .NotEmpty()
            .WithMessage("Customer Id must not be empty.");
        
        this.RuleFor(sale => sale.NewBranchId)
            .NotNull()
            .WithMessage("Branch Id must not be null.")
            .NotEmpty()
            .WithMessage("Branch Id must not be empty.");
        
        this.RuleFor(sale => sale.NewItems)
            .NotEmpty()
            .WithMessage("At least one sale item is required.")
            .Must(items => items.All(item => item.Quantity > 0))
            .WithMessage("Each sale item must have a quantity and price greater than zero.");
    }
}
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleValidator : AbstractValidator<UpdateSaleCommand>
{
    public UpdateSaleValidator()
    {
        this.RuleFor(sale => sale.Id)
            .NotEmpty().WithMessage("Sale Id is required.");
        
        this.RuleFor(sale => sale.NewSaleNumber)
            .NotEmpty().WithMessage("NewSaleNumber is required.");

        this.RuleFor(sale => sale.NewCustomerId)
            .NotEmpty().WithMessage("NewCustomerId is required.");
        
        this.RuleFor(sale => sale.NewCustomerName)
            .NotEmpty().WithMessage("NewCustomerName is required.");

        this.RuleFor(sale => sale.NewBranchId)
            .NotEmpty().WithMessage("NewBranchId is required.");
        
        this.RuleFor(sale => sale.NewBranchName)
            .NotEmpty().WithMessage("NewBranchName is required.");

        this.RuleFor(sale => sale.NewItems)
            .NotEmpty().WithMessage("At least one sale item is required.");

        this.RuleForEach(sale => sale.NewItems).ChildRules(item =>
        {
            item.RuleFor(x => x.ProductExternalId)
                .NotEmpty().WithMessage("ProductId is required.");

            item.RuleFor(x => x.Quantity)
                .InclusiveBetween(1, 20)
                .WithMessage("Quantity must be between 1 and 20.");
        });
    }
}
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    public CreateSaleRequestValidator()
    {
        this.RuleFor(sale => sale.SaleNumber)
            .NotEmpty()
            .WithMessage("Sale number must not be empty.")
            .MaximumLength(50)
            .WithMessage("Sale number must not exceed 50 characters.");
        this.RuleFor(sale => sale.CustomerId)
            .NotNull()
            .WithMessage("Customer Id must not be null.")
            .NotEmpty()
            .WithMessage("Customer Id must not be empty.");
        this.RuleFor(sale => sale.BranchId)
            .NotNull()
            .WithMessage("Branch Id must not be null.")
            .NotEmpty()
            .WithMessage("Branch Id must not be empty.");
        this.RuleFor(sale => sale.Items)
            .NotEmpty()
            .WithMessage("At least one sale item is required.");
    }
}
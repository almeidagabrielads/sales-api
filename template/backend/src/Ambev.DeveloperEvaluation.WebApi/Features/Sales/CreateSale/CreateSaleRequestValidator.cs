using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Validator for <see cref="CreateSaleRequest"/> that defines validation rules for sale creation.
/// </summary>
public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    /// <summary>
    /// Initializes a new instance of the CreateSaleRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - SaleNumber: required, maximum 50 characters.
    /// - CustomerId: required, must not be null or empty.
    /// - BranchId: required, must not be null or empty.
    /// - Items: required, must contain at least one item.
    /// </remarks>
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
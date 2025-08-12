namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

using FluentValidation;

/// <summary>
/// Validator for <see cref="CreateSaleCommand"/> that defines validation rules for creating a sale.
/// </summary>
public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleCommandValidator"/> class.
    /// Initializes a new instance of the <see cref="CreateSaleCommandValidator"/> with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - SaleNumber: Required and must not be empty
    /// - CustomerExternalId: Required and must not be empty
    /// - BranchExternalId: Required and must not be empty
    /// - Items: Must have at least one item
    /// - Each Item: ProductExternalId required, quantity must be between 1 and 20.
    /// </remarks>
    public CreateSaleCommandValidator()
    {
        this.RuleFor(sale => sale.SaleNumber)
            .NotEmpty().WithMessage("SaleNumber is required.");

        this.RuleFor(sale => sale.CustomerExternalId)
            .NotEmpty().WithMessage("CustomerId is required.");

        this.RuleFor(sale => sale.BranchExternalId)
            .NotEmpty().WithMessage("BranchId is required.");

        this.RuleFor(sale => sale.Items)
            .NotEmpty().WithMessage("At least one sale item is required.");

        this.RuleForEach(sale => sale.Items).ChildRules(item =>
        {
            item.RuleFor(x => x.ProductExternalId)
                .NotEmpty().WithMessage("ProductId is required.");

            item.RuleFor(x => x.Quantity)
                .InclusiveBetween(1, 20)
                .WithMessage("Quantity must be between 1 and 20.");
        });
    }
}
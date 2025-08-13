using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;

/// <summary>
/// Validator for <see cref="DeleteSaleRequest"/>. Ensures that all required fields are present and valid
/// when deleting a sale, including sale identifiers and sale items.
/// </summary>
public class DeleteSaleRequestValidator : AbstractValidator<DeleteSaleRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteSaleRequestValidator"/> class.
    /// Defines validation rules for deleting a sale, such as non-empty identifiers.
    /// </summary>
    public DeleteSaleRequestValidator()
    {
        this.RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Sale ID is required");
    }
}
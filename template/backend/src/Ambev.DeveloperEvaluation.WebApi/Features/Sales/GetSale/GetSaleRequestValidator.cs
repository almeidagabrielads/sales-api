using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

/// <summary>
/// Validator for <see cref="GetSaleRequest"/>. Ensures that the sale Id is provided and not empty.
/// </summary>
public class GetSaleRequestValidator : AbstractValidator<GetSaleRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetSaleRequestValidator"/> class.
    /// Sets up validation rules for the <see cref="GetSaleRequest"/> model.
    /// </summary>
    public GetSaleRequestValidator()
    {
        this.RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Sale Id is required");
    }
}
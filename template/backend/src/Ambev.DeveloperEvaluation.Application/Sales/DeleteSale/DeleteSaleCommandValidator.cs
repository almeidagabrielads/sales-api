using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

/// <summary>
/// Validator for the <see cref="DeleteSaleCommand"/>, using FluentValidation to ensure the delete sale data is correct.
/// Defines rules for required fields and validation of sale items.
/// </summary>
public class DeleteSaleCommandValidator : AbstractValidator<DeleteSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteSaleCommandValidator"/>, setting up validation rules for the delete sale command.
    /// </summary>
    public DeleteSaleCommandValidator()
    {
        // Rule: Sale Id must not be empty.
        this.RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Sale Id is required");
    }
    
}
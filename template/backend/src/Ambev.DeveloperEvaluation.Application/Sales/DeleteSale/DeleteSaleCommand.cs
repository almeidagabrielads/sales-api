using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

/// <summary>
/// Command to delete a sale, containing the sale identifier.
/// Implements <see cref="IRequest{DeleteSaleResponse}"/> to be used with MediatR for handling the delete operation.
/// </summary>
public class DeleteSaleCommand: IRequest<DeleteSaleResponse>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteSaleCommand"/> class.
    /// </summary>
    /// <param name="id">The ID of the sale to delete.</param>
    public DeleteSaleCommand(Guid id)
    {
        this.Id = id;
    }

    /// <summary>
    /// Gets the unique identifier of the sale to delete.
    /// </summary>
    public Guid Id { get; }
    
    /// <summary>
    /// Validates the command and returns detailed validation results.
    /// </summary>
    /// <returns></returns>
    public ValidationResultDetail Validate()
    {
        DeleteSaleCommandValidator validator = new DeleteSaleCommandValidator();
        FluentValidation.Results.ValidationResult result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(e => (ValidationErrorDetail)e),
        };
    }
}
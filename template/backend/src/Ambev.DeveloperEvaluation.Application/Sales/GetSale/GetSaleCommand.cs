using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Command to retrieve a sale by its unique identifier.
/// Implements <see cref="IRequest{TResponse}"/> to request a <see cref="GetSaleResult"/>.
/// </summary>
public class GetSaleCommand: IRequest<GetSaleResult>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetSaleCommand"/> class with the specified sale identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the sale to retrieve.</param>
    public GetSaleCommand(Guid id)
    {
        this.Id = id;
    }
    
    /// <summary>
    /// Gets or sets the unique identifier of the sale.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Validates the command and returns detailed validation results.
    /// </summary>
    /// <returns></returns>
    public ValidationResultDetail Validate()
    {
        GetSaleCommandValidator validator = new GetSaleCommandValidator();
        FluentValidation.Results.ValidationResult result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(e => (ValidationErrorDetail)e),
        };
    }
}
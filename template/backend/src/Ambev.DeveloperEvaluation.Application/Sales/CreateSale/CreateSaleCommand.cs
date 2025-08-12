using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;


/// <summary>
/// Command for creating a new sale.
/// </summary>
/// <remarks>
/// This command is used to create a new sale including the sale number,
/// customer and branch identifiers, and a list of items being sold.
/// The input data is validated using <see cref="CreateSaleCommandValidator"/>
/// to ensure that all required fields are populated and consistent with the business rules.
/// </remarks>
public class CreateSaleCommand : IRequest<CreateSaleResult>
{
    /// <summary>
    /// Gets or sets the unique sale number.
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the external identifier of the customer.
    /// </summary>
    public Guid CustomerExternalId { get; set; }

    /// <summary>
    /// Gets or sets the external identifier of the branch where the sale occurred.
    /// </summary>
    public Guid BranchExternalId { get; set; }

    /// <summary>
    /// Gets or sets the list of items in the sale.
    /// </summary>
    public List<CreateSaleItemDto> Items { get; set; } = new();

    /// <summary>
    /// Validates the command and returns detailed validation results.
    /// </summary>
    /// <returns></returns>
    public ValidationResultDetail Validate()
    {
        CreateSaleCommandValidator validator = new CreateSaleCommandValidator();
        FluentValidation.Results.ValidationResult result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(e => (ValidationErrorDetail)e),
        };
    }
}
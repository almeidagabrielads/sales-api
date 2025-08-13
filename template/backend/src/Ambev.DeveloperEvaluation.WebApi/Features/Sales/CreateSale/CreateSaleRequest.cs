namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Represents a request to create a sale, containing the sale number, customer and branch identifiers,
/// and a list of items to be included in the sale.
/// </summary>
public class CreateSaleRequest
{
    /// <summary>
    /// Gets or sets the sale number.
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the identifier of the customer.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the branch.
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// Gets or sets the list of items to be included in the sale.
    /// </summary>
    public List<CreateSaleItemRequest> Items { get; set; } = new();
}
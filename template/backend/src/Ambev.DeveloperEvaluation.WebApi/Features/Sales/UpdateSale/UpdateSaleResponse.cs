namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// Represents the response returned after updating a sale, including sale identifiers,
/// customer and branch information, updated items, and the total amount.
/// </summary>
public class UpdateSaleResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the updated sale.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the sale number.
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the unique identifier of the customer associated with the sale.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the branch where the sale occurred.
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// Gets or sets the list of updated sale items.
    /// </summary>
    public List<UpdateSaleItemResponse> Items { get; set; } = new();

    /// <summary>
    /// Gets or sets the total amount for the updated sale.
    /// </summary>
    public decimal TotalAmount { get; set; }
}
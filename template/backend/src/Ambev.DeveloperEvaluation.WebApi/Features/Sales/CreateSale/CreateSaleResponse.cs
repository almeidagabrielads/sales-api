namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Represents the API response model for CreateSale operation

/// </summary>
public class CreateSaleResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the sale.
    /// </summary>
    public Guid Id { get; set; }

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
    /// Gets or sets the list of items included in the sale.
    /// </summary>
    public List<CreateSaleItemResponse> Items { get; set; } = new();

    /// <summary>
    /// Gets or sets the date and time when the sale was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the total amount of the sale.
    /// </summary>
    public decimal TotalAmount { get; set; }
}
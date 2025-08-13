namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// Represents a request to update a sale, including new sale details and items.
/// </summary>
public class UpdateSaleRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the sale to be updated.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the new sale number.
    /// </summary>
    public string NewSaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the new customer identifier.
    /// </summary>
    public Guid NewCustomerId { get; set; }

    /// <summary>
    /// Gets or sets the new customer name.
    /// </summary>
    public string NewCustomerName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the new branch identifier.
    /// </summary>
    public Guid NewBranchId { get; set; }

    /// <summary>
    /// Gets or sets the new branch name.
    /// </summary>
    public string NewBranchName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of new or updated sale items.
    /// </summary>
    public List<UpdateSaleItemRequest> NewItems { get; set; } = new();
}
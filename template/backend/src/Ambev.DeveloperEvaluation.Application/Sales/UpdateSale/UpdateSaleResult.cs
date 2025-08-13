namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Represents the result of a sale update operation, containing the unique identifier of the updated sale.
/// </summary>
public class UpdateSaleResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the updated sale.
    /// </summary>
    public Guid Id { get; set; }
}
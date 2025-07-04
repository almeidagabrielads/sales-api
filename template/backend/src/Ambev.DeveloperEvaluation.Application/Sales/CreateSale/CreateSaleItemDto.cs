namespace Ambev.DeveloperEvaluation.Sales.CreateSale;

/// <summary>
/// DTO representing a single item in a sale.
/// </summary>
/// <remarks>
/// Contains the product external ID and the quantity sold.
/// Used within <see cref="CreateSaleCommand"/> to specify multiple items.
/// </remarks>
///
public class CreateSaleItemDto
{
    /// <summary>
    /// Gets or sets the external identifier of the product.
    /// </summary>
    public Guid ProductId { get; set; }
    
    /// <summary>
    /// Gets or sets the name of the product.
    /// </summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the quantity of the product sold.
    /// </summary>
    public int Quantity { get; set; }
}
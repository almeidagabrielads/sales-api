namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Represents a request to create a sale item, containing the product identifier and quantity.
/// </summary>
public class CreateSaleItemRequest
{
    /// <summary>
    /// Gets or sets the identifier of the product.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the product.
    /// </summary>
    public int Quantity { get; set; }
}
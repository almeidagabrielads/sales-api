namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Data Transfer Object (DTO) representing an item to be updated in a sale.
/// </summary>
public class UpdateSaleItemDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the sale item.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the sale to which the item belongs.
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// Gets or sets the external identifier of the product.
    /// </summary>
    public Guid ProductExternalId { get; set; }

    /// <summary>
    /// Gets or sets the unit price of the product in the sale item.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the product in the sale item.
    /// </summary>
    public int Quantity { get; set; }
}
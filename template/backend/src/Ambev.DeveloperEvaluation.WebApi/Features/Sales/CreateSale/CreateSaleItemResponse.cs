namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Represents the response when creating a sale item, containing information such as identifier,
/// product, quantity, unit price, discount, and total.
/// </summary>
public class CreateSaleItemResponse
{
    /// <summary>
    /// Gets or sets the identifier of the sale item.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the product.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the product.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the unit price of the product.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the discount percentage applied to the item.
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// Gets or sets the total value of the sale item.
    /// </summary>
    public decimal Total { get; set; }
}
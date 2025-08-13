namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
    
    /// <summary>
    /// Represents the response for an updated sale item, including product details, quantity, pricing, discount, and total.
    /// </summary>
    public class UpdateSaleItemResponse
    {
        /// <summary>
        /// Gets or sets the unique identifier of the sale item.
        /// </summary>
        public Guid Id { get; set; }
    
        /// <summary>
        /// Gets or sets the external identifier of the product.
        /// </summary>
        public Guid ProductExternalId { get; set; }
    
        /// <summary>
        /// Gets or sets the quantity of the product in the sale item.
        /// </summary>
        public int Quantity { get; set; }
    
        /// <summary>
        /// Gets or sets the unit price of the product.
        /// </summary>
        public decimal UnitPrice { get; set; }
    
        /// <summary>
        /// Gets or sets the discount applied to the sale item.
        /// </summary>
        public decimal Discount { get; set; }
    
        /// <summary>
        /// Gets or sets the total value for the sale item after discount.
        /// </summary>
        public decimal Total { get; set; }
    }
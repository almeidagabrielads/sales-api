namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;
    
    /// <summary>
    /// Data transfer object representing an item within a sale, including product details, quantity, pricing, discount, total, and cancellation status.
    /// </summary>
    public class GetSaleItemDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the sale item.
        /// </summary>
        public Guid Id { get; set; }
    
        /// <summary>
        /// Gets or sets the external unique identifier of the product associated with this sale item.
        /// </summary>
        public Guid ProductExternalId { get; set; } = Guid.Empty;
    
        /// <summary>
        /// Gets or sets the quantity of the product in this sale item.
        /// </summary>
        public int Quantity { get; set; }
    
        /// <summary>
        /// Gets or sets the unit price of the product.
        /// </summary>
        public decimal UnitPrice { get; set; }
    
        /// <summary>
        /// Gets or sets the discount applied to this sale item.
        /// </summary>
        public decimal Discount { get; set; }
    
        /// <summary>
        /// Gets the total value for this sale item after applying discounts.
        /// </summary>
        public decimal Total { get; private set; }
    
        /// <summary>
        /// Gets or sets a value indicating whether this sale item has been cancelled.
        /// </summary>
        public bool IsCancelled { get; set; } = false;
    }
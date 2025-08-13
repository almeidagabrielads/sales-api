namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;
    
    /// <summary>
    /// Represents the result of retrieving a sale, including sale details, customer and branch identifiers, items, total amount, and cancellation status.
    /// </summary>
    public class GetSaleResult
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
        /// Gets or sets the date and time when the sale was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }
    
        /// <summary>
        /// Gets or sets the external unique identifier of the customer associated with the sale.
        /// </summary>
        public Guid CustomerExternalId { get; set; }
    
        /// <summary>
        /// Gets or sets the external unique identifier of the branch where the sale occurred.
        /// </summary>
        public Guid BranchExternalId { get; set; }
    
        /// <summary>
        /// Gets or sets the list of items included in the sale.
        /// </summary>
        public virtual List<GetSaleItemDto> Items { get; set; } = new();
    
        /// <summary>
        /// Gets or sets the total amount of the sale.
        /// </summary>
        public decimal TotalAmount { get; set; }
    
        /// <summary>
        /// Gets or sets a value indicating whether the sale has been cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }
    }
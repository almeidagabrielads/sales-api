namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
    
    /// <summary>
    /// Represents the response model for retrieving a sale, including sale details, customer and branch information, items, and status.
    /// </summary>
    public class GetSaleResponse
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
        /// Gets or sets the unique identifier of the customer associated with the sale.
        /// </summary>
        public Guid CustomerId { get; set; }
    
        /// <summary>
        /// Gets or sets the name of the customer.
        /// </summary>
        public string CustomerName { get; set; } = string.Empty;
    
        /// <summary>
        /// Gets or sets the unique identifier of the branch where the sale occurred.
        /// </summary>
        public Guid BranchId { get; set; }
    
        /// <summary>
        /// Gets or sets the name of the branch.
        /// </summary>
        public string BranchName { get; set; } = string.Empty;
    
        /// <summary>
        /// Gets or sets the list of items included in the sale.
        /// </summary>
        public virtual List<GetSaleItemResponse> Items { get; set; } = new();
    
        /// <summary>
        /// Gets or sets the total amount of the sale.
        /// </summary>
        public decimal TotalAmount { get; set; }
    
        /// <summary>
        /// Gets or sets a value indicating whether the sale has been cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }
    }
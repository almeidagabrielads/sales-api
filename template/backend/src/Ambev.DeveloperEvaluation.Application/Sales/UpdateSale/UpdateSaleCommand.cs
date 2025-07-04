using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleCommand : IRequest<UpdateSaleResult>
{
    /// <summary>
    /// Gets or sets the sale Id.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Gets or sets the new sale number.
    /// </summary>
    public string NewSaleNumber { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the external identifier of the customer.
    /// </summary>
    public Guid NewCustomerId { get; set; }
    
    /// <summary>
    /// Gets or sets the name of the customer.
    /// </summary>
    public string NewCustomerName { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the external identifier of the branch where the sale occurred.
    /// </summary>
    public Guid NewBranchId { get; set; }
    
    /// <summary>
    /// Gets or sets the name of the branch where the sale occurred.
    /// </summary>
    public string NewBranchName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of items in the sale.
    /// </summary>
    public List<UpdateSaleItemDto> NewItems { get; set; } = new();
}
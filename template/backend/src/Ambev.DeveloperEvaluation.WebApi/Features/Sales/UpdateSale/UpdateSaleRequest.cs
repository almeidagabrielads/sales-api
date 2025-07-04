namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

public class UpdateSaleRequest
{
    public Guid Id { get; set; }
    public string NewSaleNumber { get; set; } = string.Empty;
    public Guid NewCustomerId { get; set; }
    public string NewCustomerName { get; set; } = string.Empty;
    public Guid NewBranchId { get; set; }
    public string NewBranchName { get; set; } = string.Empty;
    public List<UpdateSaleItemRequest> NewItems { get; set; } = new();
}
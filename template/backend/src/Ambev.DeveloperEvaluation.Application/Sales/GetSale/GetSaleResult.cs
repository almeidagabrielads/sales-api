namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

public class GetSaleResult
{
    public Guid Id { get; set; }
    public string SaleNumber { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public Guid CustomerExternalId { get; set; }
    public Guid BranchExternalId { get; set; }
    public virtual List<GetSaleItemDto> Items { get; set; } = new();
    public decimal TotalAmount { get; set; }
    public bool IsCancelled { get; set; }
}
namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

public class GetSaleItemDto
{
    public Guid Id { get; set; }
    public Guid ProductExternalId { get; set; } = Guid.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; private set; }
    public bool IsCancelled { get; set; } = false;
}
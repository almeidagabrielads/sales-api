namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

public class GetSaleItemResponse
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; } = Guid.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; private set; }
    public bool IsCancelled { get; set; } = false;
}
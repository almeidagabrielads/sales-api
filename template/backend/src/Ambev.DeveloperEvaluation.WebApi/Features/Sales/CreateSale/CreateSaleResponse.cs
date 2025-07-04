namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleResponse
{
    public Guid Id { get; set; }

    public string SaleNumber { get; set; } = string.Empty;

    public Guid CustomerId { get; set; }

    public Guid BranchId { get; set; }

    public List<CreateSaleItemResponse> Items { get; set; } = new();

    public DateTime CreatedAt { get; set; }

    public decimal TotalAmount { get; set; }
}
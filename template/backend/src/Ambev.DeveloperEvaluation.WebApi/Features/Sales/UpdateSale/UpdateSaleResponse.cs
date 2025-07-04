namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

public class UpdateSaleResponse
{
    public Guid Id { get; set; }

    public string SaleNumber { get; set; } = string.Empty;

    public Guid CustomerId { get; set; }

    public Guid BranchId { get; set; }

    public List<UpdateSaleItemResponse> Items { get; set; } = new();

    public decimal TotalAmount { get; set; }
}
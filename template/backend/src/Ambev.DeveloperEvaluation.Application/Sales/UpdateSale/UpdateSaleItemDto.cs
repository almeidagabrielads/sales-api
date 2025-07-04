namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleItemDto
{
    public Guid Id { get; set; }
    public Guid SaleId { get; set; }
    public Guid ProductExternalId { get; set; }
    public int Quantity { get; set; }
} 
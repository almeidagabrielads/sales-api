namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

public class UpdateSaleItemRequest
{
    public Guid Id { get; set; }
    public Guid SaleId { get; set; }
    public Guid ProductExternalId { get; set; }
    public int Quantity { get; set; }
}
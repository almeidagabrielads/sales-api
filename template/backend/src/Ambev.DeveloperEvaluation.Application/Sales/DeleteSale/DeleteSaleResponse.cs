namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

/// <summary>
/// Represents the response of a sale delete operation, containing the successfully status.
/// </summary>
public class DeleteSaleResponse
{
    /// <summary>
    /// Gets or sets if the deleted status was successfully.
    /// </summary>
    public bool Success { get; set; }
}
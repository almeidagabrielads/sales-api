using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Interface for the sale item repository, responsible for persistence operations related to <see cref="SaleItem"/>.
/// </summary>
public interface ISaleItemRepository
{
    /// <summary>
    /// Asynchronously saves a list of sale items.
    /// </summary>
    /// <param name="saleItems">List of sale items to be saved.</param>
    /// <param name="cancellationToken">Token to cancel the operation (optional).</param>
    /// <returns>A list of the saved sale items.</returns>
    Task<List<SaleItem>> SaveRangeAsync(List<SaleItem> saleItems, CancellationToken cancellationToken = default);
}
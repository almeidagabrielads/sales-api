using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ISaleItemRepository
{
    Task<List<SaleItem>> SaveRangeAsync(List<SaleItem> saleItems, CancellationToken cancellationToken = default);
    Task<bool> DeleteRangeAsync(List<SaleItem> items, CancellationToken cancellationToken = default);
}
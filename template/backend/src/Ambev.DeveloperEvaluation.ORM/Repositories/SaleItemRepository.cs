using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of the sale item repository, responsible for persistence operations
/// related to <see cref="SaleItem"/> using Entity Framework.
/// </summary>
public class SaleItemRepository: ISaleItemRepository
{
    /// <summary>
    /// The Entity Framework context used for database access.
    /// </summary>
    private readonly DefaultContext context;

    /// <summary>
    /// Initializes a new instance of the <see cref="SaleItemRepository"/> class.
    /// </summary>
    /// <param name="context">The Entity Framework context to be used.</param>
    public SaleItemRepository(DefaultContext context)
    {
        this.context = context;
    }

    /// <summary>
    /// Asynchronously saves a list of sale items.
    /// Adds new items or updates existing ones based on their entity state.
    /// </summary>
    /// <param name="saleItems">List of sale items to be saved.</param>
    /// <param name="cancellationToken">Token to cancel the operation (optional).</param>
    /// <returns>A list of the saved sale items.</returns>
    public async Task<List<SaleItem>> SaveRangeAsync(List<SaleItem> saleItems, CancellationToken cancellationToken = default)
    {
        foreach (var item in saleItems)
        {
            if (this.context.Entry(item).State == EntityState.Detached)
            {
                await this.context.SaleItems.AddAsync(item, cancellationToken);
            }
            else
            {
                this.context.SaleItems.Update(item);
            }
        }

        await this.context.SaveChangesAsync(cancellationToken);
        return saleItems;
    }

}
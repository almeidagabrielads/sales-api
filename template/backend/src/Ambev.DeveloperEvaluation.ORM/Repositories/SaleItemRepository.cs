using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SaleItemRepository : ISaleItemRepository
{
    private readonly DefaultContext context;

    public SaleItemRepository(DefaultContext context)
    {
        this.context = context;
    }

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
    
    public async Task<bool> DeleteRangeAsync(List<SaleItem> items, CancellationToken cancellationToken = default)
    {
        var ids = items.Select(i => i.Id).ToList();
        var existingItems = await this.context
            .SaleItems
            .Where(i => ids.Contains(i.Id))
            .ToListAsync(cancellationToken);

        var missingIds = ids.Except(existingItems.Select(i => i.Id)).ToList();
        if (missingIds.Any())
        {
            return false;
        }

        this.context.RemoveRange(existingItems);
        await this.context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
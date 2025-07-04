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
}
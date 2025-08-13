using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of the sale repository, responsible for persistence operations related to <see cref="Sale"/>.
/// </summary>
public class SaleRepository: ISaleRepository
{
    /// <summary>
    /// The Entity Framework context used for database access.
    /// </summary>
    private readonly DefaultContext context;

    /// <summary>
    /// Initializes a new instance of the <see cref="SaleRepository"/> class.
    /// </summary>
    /// <param name="context">The Entity Framework context to be used.</param>
    public SaleRepository(DefaultContext context)
    {
        this.context = context;
    }

    /// <summary>
    /// Asynchronously creates a new sale.
    /// </summary>
    /// <param name="sale">The sale entity to be created.</param>
    /// <param name="cancellationToken">Token to cancel the operation (optional).</param>
    /// <returns>The created <see cref="Sale"/> entity.</returns>
    public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        await this.context.Sales.AddAsync(sale, cancellationToken);
        await this.context.SaveChangesAsync(cancellationToken);
        return sale;
    }
    
    /// <summary>
    /// Asynchronously retrieves a sale by its sale number.
    /// </summary>
    /// <param name="saleNumber">The unique sale number to search for.</param>
    /// <param name="cancellationToken">Token to cancel the operation (optional).</param>
    /// <returns>The <see cref="Sale"/> entity if found; otherwise, null.</returns>
    public async Task<Sale?> GetBySaleNumberAsync(string saleNumber, CancellationToken cancellationToken = default)
    {
        return await this.context.Sales
            .FirstOrDefaultAsync(s => s.SaleNumber == saleNumber, cancellationToken);
    }
    
    /// <summary>
    /// Asynchronously retrieves a sale by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the sale.</param>
    /// <param name="cancellationToken">Token to cancel the operation (optional).</param>
    /// <returns>The <see cref="Sale"/> entity if found; otherwise, null.</returns>
    public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await this.context.Sales
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }
    
    /// <summary>
    /// Asynchronously updates an existing sale.
    /// </summary>
    /// <param name="sale">The sale entity to be updated.</param>
    /// <param name="cancellationToken">Token to cancel the operation (optional).</param>
    /// <returns>The updated <see cref="Sale"/> entity.</returns>
    public async Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        this.context.Sales.Update(sale);
        await this.context.SaveChangesAsync(cancellationToken);
        return sale;
    }
}
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Interface for the sale repository, responsible for persistence operations related to <see cref="Sale"/>.
/// </summary>
public interface ISaleRepository
{
    /// <summary>
    /// Asynchronously creates a new sale.
    /// </summary>
    /// <param name="sale">The sale entity to be created.</param>
    /// <param name="cancellationToken">Token to cancel the operation (optional).</param>
    /// <returns>The created <see cref="Sale"/> entity.</returns>
    Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Asynchronously retrieves a sale by its sale number.
    /// </summary>
    /// <param name="saleNumber">The unique sale number to search for.</param>
    /// <param name="cancellationToken">Token to cancel the operation (optional).</param>
    /// <returns>The <see cref="Sale"/> entity if found; otherwise, null.</returns>
    Task<Sale?> GetBySaleNumberAsync(string saleNumber, CancellationToken cancellationToken = default);    
    
   /// <summary>
   /// Asynchronously retrieves a sale by its unique identifier.
   /// </summary>
   /// <param name="id">The unique identifier of the sale.</param>
   /// <param name="cancellationToken">Token to cancel the operation (optional).</param>
   /// <returns>The <see cref="Sale"/> entity if found; otherwise, null.</returns>
   Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
   
   /// <summary>
   /// Asynchronously updates an existing sale.
   /// </summary>
   /// <param name="sale">The sale entity to be updated.</param>
   /// <param name="cancellationToken">Token to cancel the operation (optional).</param>
   /// <returns>The updated <see cref="Sale"/> entity.</returns>
   Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default);
}
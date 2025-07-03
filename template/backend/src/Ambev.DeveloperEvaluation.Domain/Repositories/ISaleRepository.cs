// <copyright file="ISaleRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

using Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Interface for persisting and retrieving sales from the data store.
/// </summary>
public interface ISaleRepository
{
    /// <summary>
    /// Persists a new sale asynchronously.
    /// </summary>
    /// <param name="sale">The sale to be persisted.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default);

    Task<Sale?> GetBySaleNumberAsync(string saleNumber, CancellationToken cancellationToken = default);
}

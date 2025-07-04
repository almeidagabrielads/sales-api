// <copyright file="SaleRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

using Microsoft.EntityFrameworkCore;

public class SaleRepository : ISaleRepository
{
    private readonly DefaultContext context;

    public SaleRepository(DefaultContext context)
    {
        this.context = context;
    }

    public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        await this.context.Sales.AddAsync(sale, cancellationToken);
        await this.context.SaveChangesAsync(cancellationToken);
        return sale;
    }

    public async Task<Sale?> GetBySaleNumberAsync(string saleNumber, CancellationToken cancellationToken = default)
    {
        return await this.context.Sales
            .FirstOrDefaultAsync(s => s.SaleNumber == saleNumber, cancellationToken);
    }
    
    public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await this.context.Sales
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }
    
    public async Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        this.context.Sales.Update(sale);
        await this.context.SaveChangesAsync(cancellationToken);
        return sale;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Sale? sale = await this.context.Sales
            .Include(s => s.Items)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        
        if (sale == null)
        {
            return false;
        }
        
        this.context.Sales.Remove(sale);
        await this.context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
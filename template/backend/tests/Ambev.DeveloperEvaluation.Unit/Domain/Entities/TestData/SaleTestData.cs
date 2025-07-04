namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

using System;
using System.Collections.Generic;

using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

/// <summary>
/// Provides methods for generating test data for Sale and SaleItem entities.
/// Centralizes generation of valid and invalid data for consistency across unit tests.
/// </summary>
public static class SaleTestData
{
    private static readonly Faker<SaleItem> SaleItemFaker = new Faker<SaleItem>()
        .RuleFor(i => i.Id, f => f.Random.Guid())
        .RuleFor(i => i.ProductExternalId, f => f.Random.Guid())
        .RuleFor(i => i.Quantity, f => f.Random.Int(1, 15))
        .RuleFor(i => i.UnitPrice, f => f.Random.Decimal(10, 100))
        .RuleFor(i => i.Discount, (f, i) => f.Random.Decimal(0, i.UnitPrice * 0.5m)) 
        .RuleFor(i => i.Total, (f, i) => i.Quantity * (i.UnitPrice - i.Discount));

    private static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
        .RuleFor(s => s.Id, f => f.Random.Guid())
        .RuleFor(s => s.SaleNumber, f => f.Random.AlphaNumeric(6))
        .RuleFor(s => s.CreatedAt, f => f.Date.Recent())
        .RuleFor(s => s.CustomerExternalId, f => f.Random.Guid())
        .RuleFor(s => s.BranchExternalId, f => f.Random.Guid())
        .RuleFor(s => s.IsCancelled, false)
        .RuleFor(s => s.Items, f => SaleItemFaker.Generate(f.Random.Int(1, 3)));

    /// <summary>
    /// Generates a valid Sale entity with randomized valid data and items.
    /// </summary>
    /// <returns>A valid Sale entity.</returns>
    public static Sale GenerateValidSale()
    {
        var sale = SaleFaker.Generate();
        foreach (var item in sale.Items)
        {
            item.SaleId = sale.Id; 
            item.ApplyDiscountRules();
        }

        sale.RecalculateTotal();
        return sale;
    }

    /// <summary>
    /// Generates a Sale entity with invalid data to test validation errors.
    /// Includes empty strings and missing required values.
    /// </summary>
    /// <returns>An invalid Sale entity.</returns>
    public static Sale GenerateInvalidSale()
    {
        return new Sale
        {
            SaleNumber = string.Empty,
            CreatedAt = DateTime.MinValue,
            CustomerExternalId = Guid.Empty,
            BranchExternalId = Guid.Empty,
            Items = new List<SaleItem>() // Empty list = invalid
        };
    }

    /// <summary>
    /// Generates a valid SaleItem entity with random values.
    /// </summary>
    /// <returns>A valid SaleItem entity.</returns>
    public static SaleItem GenerateValidSaleItem()
    {
        return SaleItemFaker.Generate();
    }

    /// <summary>
    /// Generates an invalid SaleItem entity with missing required fields or invalid values.
    /// </summary>
    /// <returns>An invalid SaleItem entity.</returns>
    public static SaleItem GenerateInvalidSaleItem()
    {
        return new SaleItem
        {
            ProductExternalId = Guid.Empty,
            Quantity = 0,
            UnitPrice = -10,
            Discount = -5
        };
    }
}
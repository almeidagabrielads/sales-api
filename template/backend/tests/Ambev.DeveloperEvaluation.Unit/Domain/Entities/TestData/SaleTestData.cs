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
        .RuleFor(i => i.ProductId, f => f.Random.Guid())
        .RuleFor(i => i.ProductName, f => f.Commerce.ProductName())
        .RuleFor(i => i.Quantity, f => f.Random.Int(1, 10))
        .RuleFor(i => i.UnitPrice, f => f.Random.Decimal(10, 100))
        .RuleFor(i => i.Discount, f => f.Random.Decimal(0, 10));

    private static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
        .RuleFor(s => s.SaleNumber, f => f.Random.AlphaNumeric(6))
        .RuleFor(s => s.CreatedAt, f => f.Date.Recent())
        .RuleFor(s => s.CustomerId, f => f.Random.Guid())
        .RuleFor(s => s.CustomerName, f => f.Name.FullName())
        .RuleFor(s => s.BranchId, f => f.Random.Guid())
        .RuleFor(s => s.BranchName, f => f.Company.CompanyName())
        .RuleFor(s => s.IsCancelled, false)
        .RuleFor(s => s.Items, f => SaleItemFaker.Generate(f.Random.Int(1, 3)));

    /// <summary>
    /// Generates a valid Sale entity with randomized valid data and items.
    /// </summary>
    /// <returns>A valid Sale entity.</returns>
    public static Sale GenerateValidSale()
    {
        return SaleFaker.Generate();
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
            CustomerId = Guid.Empty,
            CustomerName = string.Empty,
            BranchId = Guid.Empty,
            BranchName = string.Empty,
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
            ProductId = Guid.Empty,
            ProductName = string.Empty,
            Quantity = 0,
            UnitPrice = -10,
            Discount = -5
        };
    }
}
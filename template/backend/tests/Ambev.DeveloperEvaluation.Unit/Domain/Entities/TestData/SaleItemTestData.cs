namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

using System;

using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

/// <summary>
/// Provides methods for generating test data for SaleItem entities.
/// Centralizes generation of valid and invalid items for unit testing scenarios.
/// </summary>
public static class SaleItemTestData
{
    /// <summary>
    /// Faker for generating valid SaleItem instances with randomized data.
    /// </summary>
    private static readonly Faker<SaleItem> SaleItemFaker = new Faker<SaleItem>()
        .RuleFor(i => i.ProductExternalId, f => f.Random.Guid())
        .RuleFor(i => i.SaleId, f => f.Random.Guid())
        .RuleFor(i => i.Quantity, f => f.Random.Int(1, 10))
        .RuleFor(i => i.UnitPrice, f => f.Random.Decimal(10, 100))
        .RuleFor(i => i.Discount, f => f.Random.Decimal(0, 10))
        .RuleFor(i => i.IsCancelled, false);

    /// <summary>
    /// Generates a valid SaleItem entity with typical values.
    /// </summary>
    /// <returns>A valid SaleItem instance.</returns>
    public static SaleItem GenerateValidSaleItem()
    {
        return SaleItemFaker.Generate();
    }

    /// <summary>
    /// Generates an invalid SaleItem with missing or invalid properties
    /// to be used in negative test scenarios.
    /// </summary>
    /// <returns>An invalid SaleItem instance.</returns>
    public static SaleItem GenerateInvalidSaleItem()
    {
        return new SaleItem
        {
            ProductExternalId = Guid.Empty,
            Quantity = 0,
            UnitPrice = -1,
            Discount = -5,
            IsCancelled = false
        };
    }

    /// <summary>
    /// Generates a SaleItem with specified quantity for testing discount rules.
    /// </summary>
    /// <param name="quantity">The quantity to set on the item.</param>
    /// <returns>A SaleItem instance with the given quantity.</returns>
    public static SaleItem GenerateItemWithQuantity(int quantity)
    {
        var item = GenerateValidSaleItem();
        item.Quantity = quantity;
        return item;
    }
}
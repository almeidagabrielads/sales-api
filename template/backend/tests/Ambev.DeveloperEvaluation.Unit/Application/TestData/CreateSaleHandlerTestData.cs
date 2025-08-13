using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

/// <summary>
/// Provides test data generation utilities for <see cref="CreateSaleCommand"/> in unit tests.
/// </summary>
public static class CreateSaleHandlerTestData
{
    /// <summary>
    /// Configures a <see cref="Faker{T}"/> to generate valid <see cref="CreateSaleCommand"/> instances.
    /// The generated data includes:
    /// - Product ID (<see cref="Guid"/>)
    /// - Product name
    /// - Sale value (decimal between 1 and 1000)
    /// - Recent sale date
    /// </summary>
    private static readonly Faker<CreateSaleCommand> CreateSaleHandlerFaker = new Faker<CreateSaleCommand>("pt_BR")
        .RuleFor(s => s.SaleNumber, f => f.Random.AlphaNumeric(5))
        .RuleFor(s => s.CustomerExternalId, f => f.Random.Guid())
        .RuleFor(s => s.BranchExternalId, f => f.Random.Guid())
        .RuleFor(s => s.Items, f => f.Make(f.Random.Int(1, 5), () => new CreateSaleItemDto
        {
            ProductExternalId = f.Random.Guid(),
            Quantity = f.Random.Int(1, 10)
        }));

    /// <summary>
    /// Generates a valid <see cref="CreateSaleCommand"/> with random data.
    /// </summary>
    /// <returns>A valid <see cref="CreateSaleCommand"/> instance.</returns>
    public static CreateSaleCommand GenerateValidCommand()
    {
        return CreateSaleHandlerFaker.Generate();
    }
}
using System.Reflection;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Unit.Domain;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class GetSaleHandlerTestData
{
    /// <summary>
    /// Generates a valid <see cref="GetSaleCommand"/> with random data.
    /// </summary>
    /// <returns>A valid <see cref="GetSaleCommand"/> instance.</returns>
    public static GetSaleCommand GenerateValidCommand()
    {
        return new GetSaleCommand(Guid.NewGuid());
    }

    /// <summary>
    /// Generate an existing sale entity with the parameter id.
    /// </summary>
    public static Sale GenerateExistingSale(Guid id)
    {
        var sale = (Sale)Activator.CreateInstance(typeof(Sale), nonPublic: true)!;

        SetProperty(sale, nameof(Sale.Id), id);
        SetProperty(sale, nameof(Sale.SaleNumber), $"SALE-{id.ToString()[..6]}");
        SetProperty(sale, nameof(Sale.CustomerExternalId), Guid.NewGuid());
        SetProperty(sale, nameof(Sale.BranchExternalId), Guid.NewGuid());
        SetProperty(sale, nameof(Sale.Items), new List<SaleItem>());

        return sale;
    }

    /// <summary>
    /// Generate a GetSaleResult return simulating the same id.
    /// </summary>
    public static GetSaleResult GenerateResult(Guid id)
    {
        return new GetSaleResult
        {
            Id = id
        };
    }

    private static void SetProperty<T>(object obj, string propertyName, T value)
    {
        var prop = obj.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        if (prop == null)
        {
            throw new InvalidOperationException($"Property '{propertyName}' not found in {obj.GetType().Name}");
        }

        prop.SetValue(obj, value);
    }
}
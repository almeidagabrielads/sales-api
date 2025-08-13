using System.Reflection;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class DeleteSaleHandlerTestData
{
    /// <summary>
    /// Generates a valid <see cref="DeleteSaleCommand"/> with random data.
    /// </summary>
    /// <returns>A valid <see cref="DeleteSaleCommand"/> instance.</returns>
    public static DeleteSaleCommand GenerateValidCommand()
    {
        return new DeleteSaleCommand(Guid.NewGuid());
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
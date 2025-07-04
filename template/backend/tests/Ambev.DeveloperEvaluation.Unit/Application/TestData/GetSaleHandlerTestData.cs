using System.Reflection;

using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;

using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

/// <summary>
/// Utility class for generating test data for GetSaleHandler.
/// </summary>
public static class GetSaleHandlerTestData
{
    /// <summary>
    /// Gera um comando válido de busca de venda com ID aleatório.
    /// </summary>
    public static GetSaleCommand GenerateValidCommand()
    {
        return new GetSaleCommand(Guid.NewGuid());
    }

    /// <summary>
    /// Gera uma entidade Sale existente com ID fornecido, via reflection.
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
    /// Gera um resultado de GetSaleResult simulado com o mesmo ID.
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
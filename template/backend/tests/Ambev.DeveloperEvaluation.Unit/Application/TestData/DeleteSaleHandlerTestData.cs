using System.Reflection;

using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

public static class DeleteSaleHandlerTestData
{
    /// <summary>
    /// Gera um comando válido de exclusão de venda.
    /// </summary>
    public static DeleteSaleCommand GenerateValidCommand()
    {
        return new DeleteSaleCommand(Guid.NewGuid());
    }

    /// <summary>
    /// Gera uma entidade Sale simulada com construtor privado via reflection.
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
using System.Reflection;

using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;

using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

public static class UpdateSaleHandlerTestData
{
    /// <summary>
    /// Configura o Faker para gerar comandos válidos de atualização de venda (UpdateSaleCommand).
    /// Os dados gerados incluem:
    /// - Número da venda (alfanumérico)
    /// - Ids externos de cliente e filial (Guid)
    /// - Lista de itens da venda com produto, quantidade e preço unitário.
    /// </summary>
    private static readonly Faker<UpdateSaleCommand> UpdateSaleCommandFaker = new Faker<UpdateSaleCommand>("pt_BR")
        .RuleFor(s => s.Id, f => f.Random.Guid())
        .RuleFor(s => s.NewSaleNumber, f => f.Random.AlphaNumeric(6).ToUpper())
        .RuleFor(s => s.NewCustomerExternalId, f => f.Random.Guid())
        .RuleFor(s => s.NewBranchExternalId, f => f.Random.Guid())
        .RuleFor(s => s.NewItems, f => f.Make(f.Random.Int(1, 3), () => new UpdateSaleItemDto
        {
            ProductExternalId = f.Random.Guid(),
            Quantity = f.Random.Int(1, 5)
        }));

    /// <summary>
    /// Gera um comando válido de atualização de venda com dados aleatórios.
    /// </summary>
    public static UpdateSaleCommand GenerateValidCommand()
    {
        return UpdateSaleCommandFaker.Generate();
    }

    /// <summary>
    /// Gera uma entidade Sale com valores simulados, usando reflection para contornar o construtor privado.
    /// </summary>
    public static Sale GenerateExistingSale(Guid id)
    {
        var sale = (Sale)Activator.CreateInstance(typeof(Sale), nonPublic: true)!;

        SetProperty(sale, nameof(Sale.Id), id);
        SetProperty(sale, nameof(Sale.SaleNumber), $"OLD-{id.ToString()[..5]}");
        SetProperty(sale, nameof(Sale.CustomerExternalId), Guid.NewGuid());
        SetProperty(sale, nameof(Sale.BranchExternalId), Guid.NewGuid());
        SetProperty(sale, nameof(Sale.Items), new List<SaleItem>());

        return sale;
    }

    /// <summary>
    /// Gera uma lista de SaleItem a partir dos DTOs usando reflection.
    /// </summary>
    public static List<SaleItem> GenerateMappedSaleItems(List<UpdateSaleItemDto> dtoItems)
    {
        return dtoItems.Select(dto =>
        {
            var item = (SaleItem)Activator.CreateInstance(typeof(SaleItem), nonPublic: true)!;

            SetProperty(item, nameof(SaleItem.ProductExternalId), dto.ProductExternalId);
            SetProperty(item, nameof(SaleItem.Quantity), dto.Quantity);
            SetProperty(item, nameof(SaleItem.UnitPrice), dto.UnitPrice); 

            return item;
        }).ToList();
    }

    /// <summary>
    /// Gera o resultado do comando de atualização de venda.
    /// </summary>
    public static UpdateSaleResult GenerateResult(Guid id)
    {
        return new UpdateSaleResult
        {
            Id = id
        };
    }

    private static void SetProperty<T>(object obj, string propertyName, T value)
    {
        var prop = obj.GetType()
                      .GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        if (prop == null)
        {
            throw new InvalidOperationException($"Property '{propertyName}' not found in {obj.GetType().Name}");
        }

        prop.SetValue(obj, value);
    }
}
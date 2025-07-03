using Ambev.DeveloperEvaluation.Sales.CreateSale;

namespace Ambev.DeveloperEvaluation.Functional.Application.Sales.TestData;

using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Bogus;

public class CreateSaleCommandTestData
{
    /// <summary>
    /// Configura o Faker para gerar comandos válidos de venda (CreateSaleCommand).
    /// Os dados gerados incluem:
    /// - Id do produto (Guid)
    /// - Nome do produto
    /// - Valor da venda (decimal entre 1 e 1000)
    /// - Data da venda (recente).
    /// </summary>
    private static readonly Faker<CreateSaleCommand> CreateSaleHandlerFaker = new Faker<CreateSaleCommand>("pt_BR")
        .RuleFor(s => s.SaleNumber, f => f.Random.AlphaNumeric(5))
        .RuleFor(s => s.CustomerId, f => f.Random.Guid())
        .RuleFor(s => s.CustomerName, f => f.Person.FullName)
        .RuleFor(s => s.BranchId, f => f.Random.Guid())
        .RuleFor(s => s.BranchName, f => f.Company.CompanyName())
        .RuleFor(s => s.Items, f => f.Make(f.Random.Int(1, 5), () => new CreateSaleItemDto
        {
            ProductId = f.Random.Guid(),
            ProductName = f.Commerce.ProductName(),
            Quantity = f.Random.Int(1, 10)
        }));
    
    /// <summary>
    /// Gera um comando válido de venda com dados aleatórios.
    /// </summary>
    /// <returns>Comando de venda válido.</returns>
    public static CreateSaleCommand GenerateValidCommand()
    {
        return CreateSaleHandlerFaker.Generate();
    }

}
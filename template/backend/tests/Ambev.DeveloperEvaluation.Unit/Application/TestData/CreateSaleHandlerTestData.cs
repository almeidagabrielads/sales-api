using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Sales.CreateSale;

using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain;

public static class CreateSaleHandlerTestData
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
            .RuleFor(s => s.CustomerExternalId, f => f.Random.Guid())
            .RuleFor(s => s.BranchExternalId, f => f.Random.Guid())
            .RuleFor(s => s.Items, f => f.Make(f.Random.Int(1, 5), () => new CreateSaleItemDto
            {
                ProductExternalId = f.Random.Guid(),
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
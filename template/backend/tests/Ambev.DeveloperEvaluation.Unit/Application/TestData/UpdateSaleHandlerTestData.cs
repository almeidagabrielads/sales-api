using System.Reflection;
    using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
    using Ambev.DeveloperEvaluation.Domain.Entities;
    using Bogus;
    
    namespace Ambev.DeveloperEvaluation.Unit.Domain;
    
    /// <summary>
    /// Provides test data generation utilities for unit tests related to updating sales.
    /// Includes methods to generate valid update commands, mock Sale entities, mapped SaleItems, and result objects.
    /// </summary>
    public class UpdateSaleHandlerTestData
    {
        /// <summary>
        /// Configures a Faker to generate valid <see cref="UpdateSaleCommand"/> instances with random but valid data.
        /// The generated data includes:
        /// - Sale number (alphanumeric)
        /// - External customer and branch IDs (Guid)
        /// - List of sale items with product, quantity, and unit price.
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
        /// Generates a valid <see cref="UpdateSaleCommand"/> with random data.
        /// </summary>
        /// <returns>A valid <see cref="UpdateSaleCommand"/> instance.</returns>
        public static UpdateSaleCommand GenerateValidCommand()
        {
            return UpdateSaleCommandFaker.Generate();
        }
    
        /// <summary>
        /// Generates a <see cref="Sale"/> entity with simulated values, using reflection to bypass the private constructor.
        /// </summary>
        /// <param name="id">The unique identifier for the sale.</param>
        /// <returns>A <see cref="Sale"/> entity with preset properties.</returns>
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
        /// Generates a list of <see cref="SaleItem"/> entities from a list of <see cref="UpdateSaleItemDto"/>s using reflection.
        /// </summary>
        /// <param name="dtoItems">The list of DTOs to map.</param>
        /// <returns>A list of <see cref="SaleItem"/> entities.</returns>
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
        /// Generates an <see cref="UpdateSaleResult"/> for a given sale ID.
        /// </summary>
        /// <param name="id">The unique identifier for the sale.</param>
        /// <returns>An <see cref="UpdateSaleResult"/> instance.</returns>
        public static UpdateSaleResult GenerateResult(Guid id)
        {
            return new UpdateSaleResult
            {
                Id = id
            };
        }
    
        /// <summary>
        /// Sets the value of a property on an object using reflection.
        /// </summary>
        /// <typeparam name="T">The type of the value to set.</typeparam>
        /// <param name="obj">The object whose property will be set.</param>
        /// <param name="propertyName">The name of the property to set.</param>
        /// <param name="value">The value to assign to the property.</param>
        /// <exception cref="InvalidOperationException">Thrown if the property is not found.</exception>
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
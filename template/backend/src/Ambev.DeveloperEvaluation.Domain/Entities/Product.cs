using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string ExternalId { get; private set; } = string.Empty;
        public string Name { get; private set; } = string.Empty;
        public decimal UnitPrice { get; private set; }

        public Product(string externalId, string name, decimal unitPrice)
        {
            if (string.IsNullOrWhiteSpace(externalId))
                throw new ArgumentException("ExternalId is required.");
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.");
            if (unitPrice <= 0)
                throw new ArgumentException("UnitPrice must be greater than 0.");

            Id = Guid.NewGuid();
            ExternalId = externalId;
            Name = name;
            UnitPrice = unitPrice;
        }
    }
}
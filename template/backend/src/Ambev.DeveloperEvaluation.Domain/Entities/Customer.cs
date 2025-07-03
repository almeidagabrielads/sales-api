using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Customer : BaseEntity
{
    public string ExternalId { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;

    public Customer(string externalId, string name)
    {
        if (string.IsNullOrWhiteSpace(externalId))
            throw new ArgumentException("ExternalId is required.");
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.");

        Id = Guid.NewGuid();
        ExternalId = externalId;
        Name = name;
    }
}
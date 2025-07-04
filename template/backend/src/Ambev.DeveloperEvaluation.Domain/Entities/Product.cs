using Ambev.DeveloperEvaluation.Application.Validators;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Product : BaseEntity
{
    public Product()
    {
    }

    public string ExternalId { get; private set; } = string.Empty;

    public string Name { get; private set; } = string.Empty;

    public decimal UnitPrice { get; private set; }

    public ValidationResultDetail Validate()
    {
        ProductValidator validator = new ProductValidator();
        FluentValidation.Results.ValidationResult result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(e => (ValidationErrorDetail)e),
        };
    }
}
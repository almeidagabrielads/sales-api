using Ambev.DeveloperEvaluation.Application.Validators;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Branch : BaseEntity
{
    public Branch()
    {
    }

    public string ExternalId { get; private set; } = string.Empty;

    public string Name { get; private set; } = string.Empty;

    public ValidationResultDetail Validate()
    {
        BranchValidator validator = new BranchValidator();
        FluentValidation.Results.ValidationResult result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(e => (ValidationErrorDetail)e),
        };
    }
}
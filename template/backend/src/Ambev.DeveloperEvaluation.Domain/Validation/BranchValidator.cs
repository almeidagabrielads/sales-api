namespace Ambev.DeveloperEvaluation.Application.Validators;

using Ambev.DeveloperEvaluation.Domain.Entities;

using FluentValidation;

public class BranchValidator : AbstractValidator<Branch>
{
    public BranchValidator()
    {
        this.RuleFor(branch => branch.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("Branch Id must be valid.");

        this.RuleFor(branch => branch.ExternalId)
            .NotEmpty()
            .WithMessage("Branch ExternalId is required.")
            .MaximumLength(50)
            .WithMessage("Branch ExternalId must not exceed 50 characters.");

        this.RuleFor(branch => branch.Name)
            .NotEmpty()
            .WithMessage("Branch name must not be empty.")
            .MaximumLength(500)
            .WithMessage("Branch name must not exceed 500 characters.");
    }
}

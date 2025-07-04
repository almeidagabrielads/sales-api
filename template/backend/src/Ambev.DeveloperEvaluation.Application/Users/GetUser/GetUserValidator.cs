namespace Ambev.DeveloperEvaluation.Application.Users.GetUser;

using FluentValidation;

/// <summary>
/// Validator for GetUserCommand.
/// </summary>
public class GetUserValidator : AbstractValidator<GetUserCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetUserValidator"/> class.
    /// Initializes validation rules for GetUserCommand.
    /// </summary>
    public GetUserValidator()
    {
        this.RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("User ID is required");
    }
}

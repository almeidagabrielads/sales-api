namespace Ambev.DeveloperEvaluation.Application.Users.DeleteUser;

using FluentValidation;

/// <summary>
/// Validator for DeleteUserCommand.
/// </summary>
public class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteUserValidator"/> class.
    /// Initializes validation rules for DeleteUserCommand.
    /// </summary>
    public DeleteUserValidator()
    {
        this.RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("User ID is required");
    }
}

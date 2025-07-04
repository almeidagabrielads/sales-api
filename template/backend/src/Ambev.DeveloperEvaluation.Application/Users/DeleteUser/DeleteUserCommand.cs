namespace Ambev.DeveloperEvaluation.Application.Users.DeleteUser;

using MediatR;

/// <summary>
/// Command for deleting a user.
/// </summary>
public record DeleteUserCommand : IRequest<DeleteUserResponse>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteUserCommand"/> class.
    /// Initializes a new instance of DeleteUserCommand.
    /// </summary>
    /// <param name="id">The ID of the user to delete.</param>
    public DeleteUserCommand(Guid id)
    {
        this.Id = id;
    }

    /// <summary>
    /// Gets the unique identifier of the user to delete.
    /// </summary>
    public Guid Id { get; }
}

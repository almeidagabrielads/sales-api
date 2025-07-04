namespace Ambev.DeveloperEvaluation.Application.Users.GetUser;

using MediatR;

/// <summary>
/// Command for retrieving a user by their ID.
/// </summary>
public record GetUserCommand : IRequest<GetUserResult>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetUserCommand"/> class.
    /// Initializes a new instance of GetUserCommand.
    /// </summary>
    /// <param name="id">The ID of the user to retrieve.</param>
    public GetUserCommand(Guid id)
    {
        this.Id = id;
    }

    /// <summary>
    /// Gets the unique identifier of the user to retrieve.
    /// </summary>
    public Guid Id { get; }
}

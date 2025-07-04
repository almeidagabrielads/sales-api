namespace Ambev.DeveloperEvaluation.Application.Users.GetUser;

using Ambev.DeveloperEvaluation.Domain.Enums;

/// <summary>
/// Response model for GetUser operation.
/// </summary>
public class GetUserResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the user.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the user's full name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's email address.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's phone number.
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's role in the system.
    /// </summary>
    public UserRole Role { get; set; }

    /// <summary>
    /// Gets or sets the current status of the user.
    /// </summary>
    public UserStatus Status { get; set; }
}

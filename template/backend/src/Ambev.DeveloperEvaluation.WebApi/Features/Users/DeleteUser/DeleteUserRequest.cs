// <copyright file="DeleteUserRequest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.DeleteUser;

/// <summary>
/// Request model for deleting a user.
/// </summary>
public class DeleteUserRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the user to delete.
    /// </summary>
    public Guid Id { get; set; }
}

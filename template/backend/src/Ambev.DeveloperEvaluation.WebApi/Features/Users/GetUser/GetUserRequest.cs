// <copyright file="GetUserRequest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;

/// <summary>
/// Request model for getting a user by ID.
/// </summary>
public class GetUserRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the user to retrieve.
    /// </summary>
    public Guid Id { get; set; }
}

// <copyright file="GetUserProfile.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ambev.DeveloperEvaluation.Application.Users.GetUser;

using Ambev.DeveloperEvaluation.Domain.Entities;

using AutoMapper;

/// <summary>
/// Profile for mapping between User entity and GetUserResponse.
/// </summary>
public class GetUserProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetUserProfile"/> class.
    /// Initializes the mappings for GetUser operation.
    /// </summary>
    public GetUserProfile()
    {
        this.CreateMap<User, GetUserResult>();
    }
}

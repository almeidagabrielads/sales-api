// <copyright file="CreateUserProfile.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser;

using Ambev.DeveloperEvaluation.Domain.Entities;

using AutoMapper;

/// <summary>
/// Profile for mapping between User entity and CreateUserResponse.
/// </summary>
public class CreateUserProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateUserProfile"/> class.
    /// Initializes the mappings for CreateUser operation.
    /// </summary>
    public CreateUserProfile()
    {
        this.CreateMap<CreateUserCommand, User>();
        this.CreateMap<User, CreateUserResult>();
    }
}

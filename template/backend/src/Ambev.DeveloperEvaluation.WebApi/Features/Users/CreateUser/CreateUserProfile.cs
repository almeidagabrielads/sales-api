// <copyright file="CreateUserProfile.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;

using Ambev.DeveloperEvaluation.Application.Users.CreateUser;

using AutoMapper;

/// <summary>
/// Profile for mapping between Application and API CreateUser responses.
/// </summary>
public class CreateUserProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateUserProfile"/> class.
    /// </summary>
    public CreateUserProfile()
    {
        this.CreateMap<CreateUserRequest, CreateUserCommand>();
        this.CreateMap<CreateUserResult, CreateUserResponse>();
    }
}

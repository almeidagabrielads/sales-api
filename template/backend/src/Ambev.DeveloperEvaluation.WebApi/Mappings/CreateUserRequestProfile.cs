// <copyright file="CreateUserRequestProfile.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ambev.DeveloperEvaluation.WebApi.Mappings;

using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;

using AutoMapper;

public class CreateUserRequestProfile : Profile
{
    public CreateUserRequestProfile()
    {
        this.CreateMap<CreateUserRequest, CreateUserCommand>();
    }
}
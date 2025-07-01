// <copyright file="GetUserProfile.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;

using AutoMapper;

/// <summary>
/// Profile for mapping GetUser feature requests to commands.
/// </summary>
public class GetUserProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetUserProfile"/> class.
    /// Initializes the mappings for GetUser feature.
    /// </summary>
    public GetUserProfile()
    {
        this.CreateMap<Guid, Application.Users.GetUser.GetUserCommand>()
            .ConstructUsing(id => new Application.Users.GetUser.GetUserCommand(id));
    }
}

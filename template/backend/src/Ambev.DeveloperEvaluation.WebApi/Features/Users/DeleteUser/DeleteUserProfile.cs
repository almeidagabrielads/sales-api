// <copyright file="DeleteUserProfile.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.DeleteUser;

using AutoMapper;

/// <summary>
/// Profile for mapping DeleteUser feature requests to commands.
/// </summary>
public class DeleteUserProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteUserProfile"/> class.
    /// Initializes the mappings for DeleteUser feature.
    /// </summary>
    public DeleteUserProfile()
    {
        this.CreateMap<Guid, Application.Users.DeleteUser.DeleteUserCommand>()
            .ConstructUsing(id => new Application.Users.DeleteUser.DeleteUserCommand(id));
    }
}

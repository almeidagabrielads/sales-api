// <copyright file="ApplicationModuleInitializer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers;

using Ambev.DeveloperEvaluation.Common.Security;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

public class ApplicationModuleInitializer : IModuleInitializer
{
    public void Initialize(WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();
    }
}
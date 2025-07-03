// <copyright file="IModuleInitializer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ambev.DeveloperEvaluation.IoC;

using Microsoft.AspNetCore.Builder;

public interface IModuleInitializer
{
    void Initialize(WebApplicationBuilder builder);
}

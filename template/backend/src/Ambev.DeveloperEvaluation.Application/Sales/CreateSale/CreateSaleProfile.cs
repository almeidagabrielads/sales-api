// <copyright file="CreateSaleProfile.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

using Ambev.DeveloperEvaluation.Domain.Entities;

using AutoMapper;

/// <summary>
/// Profile for mapping between Sale entity and CreateSaleResponse.
/// </summary>
public class CreateSaleProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleProfile"/> class.
    /// Initializes the mappings for CreateSale operation.
    /// </summary>
    public CreateSaleProfile()
    {
        this.CreateMap<CreateSaleCommand, Sale>()
            .ReverseMap();
    }
}

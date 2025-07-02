// <copyright file="CreateSaleHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

using AutoMapper;

using FluentValidation;

using MediatR;

public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository saleRepository;
    private readonly IMapper mapper;

    public CreateSaleHandler(
        ISaleRepository saleRepository,
        IMapper mapper)
    {
        this.saleRepository = saleRepository;
        this.mapper = mapper;
    }

    public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        CreateSaleCommandValidator validator = new CreateSaleCommandValidator();
        FluentValidation.Results.ValidationResult validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        Sale? existingSale = await this.saleRepository.GetBySaleNumberAsync(command.SaleNumber, cancellationToken);
        if (existingSale != null)
        {
            throw new InvalidOperationException($"Sale with SaleNumber {command.SaleNumber} already exists");
        }

        Sale sale = this.mapper.Map<Sale>(command);
        sale.Items.ForEach(i => i.ApplyDiscountRules());
        sale.RecalculateTotal();

        Sale createdSale = await this.saleRepository.CreateAsync(sale, cancellationToken);
        CreateSaleResult result = this.mapper.Map<CreateSaleResult>(createdSale);
        return result;
    }
}

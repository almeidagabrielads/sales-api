using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository saleRepository;
    private readonly ISaleItemRepository saleItemRepository;
    private readonly IMapper mapper;

    public CreateSaleHandler(
        ISaleRepository saleRepository,
        ISaleItemRepository saleItemRepository,
        IMapper mapper)
    {
        this.saleRepository = saleRepository;
        this.saleItemRepository = saleItemRepository;
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
        Sale createdSale = await this.saleRepository.CreateAsync(sale, cancellationToken);

        foreach (var saleItem in createdSale.Items)
        {
            saleItem.ApplyDiscountRules();
            saleItem.SaleId = createdSale.Id;
        }
        
        createdSale.RecalculateTotal();
        List<SaleItem> saleItems = this.mapper.Map<List<SaleItem>>(sale.Items);
        await this.saleItemRepository.SaveRangeAsync(saleItems, cancellationToken);
        
        CreateSaleResult result = this.mapper.Map<CreateSaleResult>(createdSale);
        return result;
    }
}
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

using AutoMapper;

using FluentValidation;

using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly ISaleRepository saleRepository;
    private readonly ISaleItemRepository saleItemRepository;
    private readonly IMapper mapper;
    
    public UpdateSaleHandler(
        ISaleRepository saleRepository,
        ISaleItemRepository saleItemRepository,
        IMapper mapper)
    {
        this.saleRepository = saleRepository;
        this.saleItemRepository = saleItemRepository;
        this.mapper = mapper;
    }
    
    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
    {
        UpdateSaleValidator validator = new();
        FluentValidation.Results.ValidationResult validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        Sale? existingSale = await this.saleRepository.GetByIdAsync(command.Id, cancellationToken);
        if (existingSale == null)
        {
            throw new InvalidOperationException($"Sale with Id {command.Id} does not exist");
        }
        
        List<SaleItem> items = this.mapper.Map<List<SaleItem>>(command.NewItems);
        existingSale.UpdateDetails(command.NewSaleNumber, command.NewCustomerExternalId, command.NewBranchExternalId, items);
        
        Sale updatedSale = await this.saleRepository.UpdateAsync(existingSale, cancellationToken);
        updatedSale.Items.ForEach(i => i.ApplyDiscountRules());
        await this.saleItemRepository.SaveRangeAsync(updatedSale.Items, cancellationToken);
        
        UpdateSaleResult result = this.mapper.Map<UpdateSaleResult>(updatedSale);
        return result;
    }
}
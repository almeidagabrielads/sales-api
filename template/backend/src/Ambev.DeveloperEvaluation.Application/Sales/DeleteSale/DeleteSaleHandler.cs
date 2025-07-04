using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

using FluentValidation;

using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, DeleteSaleResponse>
{
    private readonly ISaleRepository saleRepository;
    private readonly ISaleItemRepository saleItemRepository;
    
    public DeleteSaleHandler(ISaleRepository saleRepository, ISaleItemRepository saleItemRepository)
    {
        this.saleRepository = saleRepository;
        this.saleItemRepository = saleItemRepository;
    }
    
    public async Task<DeleteSaleResponse> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
    {
        DeleteSaleValidator validator = new DeleteSaleValidator();
        FluentValidation.Results.ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);
        bool success = false;
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        Sale? sale = await this.saleRepository.GetByIdAsync(request.Id, cancellationToken);
        if (sale == null)
        {
            throw new KeyNotFoundException($"Sale with ID {request.Id} not found");
        }

        bool deleteItemsSuccess = await this.saleItemRepository.DeleteRangeAsync(sale.Items, cancellationToken);
        if (!deleteItemsSuccess)
        {
            throw new Exception("Failed to delete sale items");
        }
        else
        {
            await this.saleRepository.DeleteAsync(request.Id, cancellationToken);
            success = true;
        }
        
        return new DeleteSaleResponse { Success = success };
    }
}
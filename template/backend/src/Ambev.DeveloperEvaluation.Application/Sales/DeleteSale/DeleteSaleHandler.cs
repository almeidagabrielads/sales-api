using Ambev.DeveloperEvaluation.Application.Sales.Events;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

public class DeleteSaleHandler: IRequestHandler<DeleteSaleCommand, DeleteSaleResponse>
{
    private readonly ISaleRepository saleRepository;
    private readonly ISaleItemRepository saleItemRepository;
    private readonly IMediator _mediator;
    
    public DeleteSaleHandler(ISaleRepository saleRepository, ISaleItemRepository saleItemRepository,
        IMediator mediator)
    {
        this.saleRepository = saleRepository;
        this.saleItemRepository = saleItemRepository;
        this._mediator = mediator;
    }
    
    public async Task<DeleteSaleResponse> Handle(DeleteSaleCommand command, CancellationToken cancellationToken)
    {
        DeleteSaleCommandValidator validator = new DeleteSaleCommandValidator();
        FluentValidation.Results.ValidationResult validationResult = await validator.ValidateAsync(command, cancellationToken);
        bool success = false;
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        Sale? sale = await this.saleRepository.GetByIdAsync(command.Id, cancellationToken);
        if (sale == null)
        {
            throw new KeyNotFoundException($"Sale with ID {command.Id} not found");
        }

        bool deleteItemsSuccess = await this.saleItemRepository.DeleteRangeAsync(sale.Items, cancellationToken);
        if (!deleteItemsSuccess)
        {
            throw new Exception("Failed to delete sale items");
        }
        
        await _mediator.Publish(new ItemCancelledEvent(command.Id), cancellationToken);

        await this.saleRepository.DeleteAsync(command.Id, cancellationToken);
        success = true;
        
        await _mediator.Publish(new SaleCancelledEvent(command.Id), cancellationToken);

        return new DeleteSaleResponse { Success = success };
    }
    
}
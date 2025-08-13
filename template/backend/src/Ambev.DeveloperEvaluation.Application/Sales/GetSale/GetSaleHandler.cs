using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

public class GetSaleHandler: IRequestHandler<GetSaleCommand, GetSaleResult>
{
    private readonly ISaleRepository saleRepository;
    private readonly IMapper mapper;
    
    public GetSaleHandler(
        ISaleRepository saleRepository,
        IMapper mapper)
    {
        this.saleRepository = saleRepository;
        this.mapper = mapper;
    }

    public async Task<GetSaleResult> Handle(GetSaleCommand request, CancellationToken cancellationToken)
    {
        GetSaleCommandValidator validator = new GetSaleCommandValidator();
        FluentValidation.Results.ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        Sale? sale = await this.saleRepository.GetByIdAsync(request.Id, cancellationToken);
        if (sale == null)
        {
            throw new KeyNotFoundException($"Sale with ID {request.Id} not found");
        }
        
        return this.mapper.Map<GetSaleResult>(sale);
    }
    
}
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleProfile : Profile
{
    public CreateSaleProfile()
    {
        this.CreateMap<CreateSaleItemRequest, CreateSaleItemDto>();
        this.CreateMap<CreateSaleRequest, CreateSaleCommand>()
            .ReverseMap();
        this.CreateMap<CreateSaleResult, CreateSaleResponse>()
            .ReverseMap();
    }
}
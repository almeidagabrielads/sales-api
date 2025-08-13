using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

public class UpdateSaleProfile: Profile
{
    public UpdateSaleProfile()
    {
        this.CreateMap<UpdateSaleItemRequest, UpdateSaleItemDto>();
        this.CreateMap<UpdateSaleRequest, UpdateSaleCommand>();
        this.CreateMap<UpdateSaleResult, UpdateSaleResponse>();
    }
}
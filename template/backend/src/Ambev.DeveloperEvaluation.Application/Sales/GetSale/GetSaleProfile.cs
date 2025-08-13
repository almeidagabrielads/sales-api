using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

public class GetSaleProfile : Profile
{
    public GetSaleProfile()
    {
        this.CreateMap<Sale, GetSaleResult>();
        this.CreateMap<SaleItem, GetSaleItemDto>();
    }
}
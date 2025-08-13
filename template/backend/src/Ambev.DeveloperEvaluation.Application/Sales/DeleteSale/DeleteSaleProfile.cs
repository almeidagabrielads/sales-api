using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

public class DeleteSaleProfile: Profile
{
    public DeleteSaleProfile()
    {
        this.CreateMap<Sale, DeleteSaleResponse>();
    }
    
}
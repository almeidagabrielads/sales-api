using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

public class GetSaleCommand : IRequest<GetSaleResult>
{
    public GetSaleCommand(Guid id)
    {
        this.Id = id;
    }
    
    public Guid Id { get; set; }
}
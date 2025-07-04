using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

public class DeleteSaleCommand : IRequest<DeleteSaleResponse>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteSaleCommand"/> class.
    /// </summary>
    /// <param name="id">The ID of the sale to delete.</param>
    public DeleteSaleCommand(Guid id)
    {
        this.Id = id;
    }

    /// <summary>
    /// Gets the unique identifier of the sale to delete.
    /// </summary>
    public Guid Id { get; }
}
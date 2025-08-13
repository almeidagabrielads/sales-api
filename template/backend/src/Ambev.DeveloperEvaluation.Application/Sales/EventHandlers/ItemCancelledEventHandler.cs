using Ambev.DeveloperEvaluation.Application.Sales.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.EventHandlers;

public class ItemCancelledEventHandler(ILogger<ItemCancelledEventHandler> logger)
    : INotificationHandler<ItemCancelledEvent>
{
    public Task Handle(ItemCancelledEvent n, CancellationToken ct)
    {
        logger.LogInformation("ItemsCancelled | SaleId={SaleId}", n.SaleId);
        return Task.CompletedTask;
    }
}
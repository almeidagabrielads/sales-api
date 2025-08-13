using Ambev.DeveloperEvaluation.Application.Sales.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.EventHandlers;

public class SaleModifiedEventHandler(ILogger<SaleModifiedEventHandler> logger)
    : INotificationHandler<SaleModifiedEvent>
{
    public Task Handle(SaleModifiedEvent n, CancellationToken ct)
    {
        logger.LogInformation("SaleModified | SaleId={SaleId}", n.SaleId);
        return Task.CompletedTask;
    }
    
}
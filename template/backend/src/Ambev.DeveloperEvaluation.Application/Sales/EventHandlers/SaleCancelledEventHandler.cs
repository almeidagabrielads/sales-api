using Ambev.DeveloperEvaluation.Application.Sales.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.EventHandlers;

public class SaleCancelledEventHandler(ILogger<SaleCancelledEventHandler> logger)
    : INotificationHandler<SaleCancelledEvent>
{
    public Task Handle(SaleCancelledEvent n, CancellationToken ct)
    {
        logger.LogInformation("SaleCancelled | SaleId={SaleId}", n.SaleId);
        return Task.CompletedTask;
    }
    
}
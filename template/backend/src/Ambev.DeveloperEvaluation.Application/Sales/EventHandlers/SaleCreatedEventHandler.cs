using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.Events;

public class SaleCreatedEventHandler(ILogger<SaleCreatedEventHandler> logger) : INotificationHandler<SaleCreatedEvent>
{
    public Task Handle(SaleCreatedEvent notification, CancellationToken ct)
    {
        logger.LogInformation("SaleCreated | SaleId={SaleId} | SaleNumber={SaleNumber}",
            notification.SaleId, notification.SaleNumber);
        return Task.CompletedTask;
    }
}
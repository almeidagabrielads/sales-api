using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.Events;

public class SaleCreatedEventHandler: INotificationHandler<SaleCreatedEvent>
{
    private readonly ILogger<SaleCreatedEventHandler> _logger;

    public SaleCreatedEventHandler(ILogger<SaleCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(SaleCreatedEvent notification, CancellationToken ct)
    {
        _logger.LogInformation("SaleCreated | SaleId={SaleId} | SaleNumber={SaleNumber}",
            notification.SaleId, notification.SaleNumber);
        return Task.CompletedTask;
    }
}
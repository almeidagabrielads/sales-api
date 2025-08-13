using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Events;

public record SaleCreatedEvent(Guid SaleId, string SaleNumber) : INotification;
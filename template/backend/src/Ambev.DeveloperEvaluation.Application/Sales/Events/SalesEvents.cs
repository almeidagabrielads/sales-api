using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Events;

public record SaleCreatedEvent(Guid SaleId, string SaleNumber) : INotification;
public record SaleModifiedEvent(Guid SaleId) : INotification;
public record SaleCancelledEvent(Guid SaleId) : INotification;
public record ItemCancelledEvent(Guid SaleId, Guid ItemId) : INotification;
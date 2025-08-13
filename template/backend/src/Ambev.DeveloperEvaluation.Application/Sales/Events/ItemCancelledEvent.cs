using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Events;

public record ItemCancelledEvent(Guid SaleId) : INotification;
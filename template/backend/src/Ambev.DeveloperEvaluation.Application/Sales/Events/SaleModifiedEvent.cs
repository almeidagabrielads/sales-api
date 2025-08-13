using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Events;

public record SaleModifiedEvent(Guid SaleId) : INotification;
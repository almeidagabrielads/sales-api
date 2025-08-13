using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Events;

public record SaleCancelledEvent(Guid SaleId) : INotification;
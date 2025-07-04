using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

public class DeleteSaleValidator : AbstractValidator<DeleteSaleCommand>
{
    public DeleteSaleValidator()
    {
        this.RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Sale Id is required");
    }
}
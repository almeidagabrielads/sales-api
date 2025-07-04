using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

public class GetSaleValidator : AbstractValidator<GetSaleCommand>
{
    public GetSaleValidator()
    {
        this.RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Sale Id is required");
    }
}
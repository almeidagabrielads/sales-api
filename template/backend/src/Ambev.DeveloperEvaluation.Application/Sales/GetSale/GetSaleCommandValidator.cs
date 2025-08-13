using FluentValidation;
    
    namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;
    
    /// <summary>
    /// Validator for the <see cref="GetSaleCommand"/> class.
    /// Ensures that the sale identifier is provided and not empty.
    /// </summary>
    public class GetSaleCommandValidator: AbstractValidator<GetSaleCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetSaleCommandValidator"/> class.
        /// Sets up validation rules for the <see cref="GetSaleCommand"/>.
        /// </summary>
        public GetSaleCommandValidator()
        {
            this.RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Sale Id is required");
        }
    }
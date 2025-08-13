        namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
        using FluentValidation;
        
        /// <summary>
        /// Validator for the <see cref="UpdateSaleCommand"/>, using FluentValidation to ensure the update sale data is correct.
        /// Defines rules for required fields and validation of sale items.
        /// </summary>
        public class UpdateSaleCommandValidator: AbstractValidator<UpdateSaleCommand>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="UpdateSaleCommandValidator"/>, setting up validation rules for the update sale command.
            /// </summary>
            public UpdateSaleCommandValidator()
            {
                // Rule: Sale Id must not be empty.
                this.RuleFor(sale => sale.Id)
                    .NotEmpty().WithMessage("Sale Id is required.");
                
                // Rule: New sale number must not be empty.
                this.RuleFor(sale => sale.NewSaleNumber)
                    .NotEmpty().WithMessage("NewSaleNumber is required.");
        
                // Rule: New customer external Id must not be empty.
                this.RuleFor(sale => sale.NewCustomerExternalId)
                    .NotEmpty().WithMessage("NewCustomerId is required.");
        
                // Rule: New branch external Id must not be empty.
                this.RuleFor(sale => sale.NewBranchExternalId)
                    .NotEmpty().WithMessage("NewBranchId is required.");
        
                // Rule: There must be at least one sale item.
                this.RuleFor(sale => sale.NewItems)
                    .NotEmpty().WithMessage("At least one sale item is required.");
        
                // Rules for each sale item.
                this.RuleForEach(sale => sale.NewItems).ChildRules(item =>
                {
                    // Rule: Product external Id must not be empty.
                    item.RuleFor(x => x.ProductExternalId)
                        .NotEmpty().WithMessage("ProductId is required.");
        
                    // Rule: Quantity must be between 1 and 20.
                    item.RuleFor(x => x.Quantity)
                        .InclusiveBetween(1, 20)
                        .WithMessage("Quantity must be between 1 and 20.");
                });
            }
            
        }
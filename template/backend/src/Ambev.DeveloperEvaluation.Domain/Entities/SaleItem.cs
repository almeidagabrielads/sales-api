using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class SaleItem : BaseEntity
{
    public Guid ProductId { get; set; } = Guid.Empty;
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; private set; }
    public bool IsCancelled { get; set; } = false;
    public Guid SaleId { get; set; }

    public void ApplyDiscountRules()
    {
        this.Discount = this.Quantity switch
        {
            >= 10 => 0.20m,
            >= 4 => 0.10m,
            _ => 0.0m,
        };
        
        this.Total = this.CalculateTotal();
    }

    public void Cancel()
    {
        if (this.IsCancelled)
        {
            throw new InvalidOperationException("Item is already cancelled.");
        }

        this.IsCancelled = true;
    }
    
    public ValidationResultDetail Validate()
    {
        SaleItemValidator validator = new SaleItemValidator();
        FluentValidation.Results.ValidationResult result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(e => (ValidationErrorDetail)e),
        };
    }

    private decimal CalculateTotal()
    {
        decimal gross = this.Quantity * this.UnitPrice;
        decimal discountAmount = gross * this.Discount;
        return gross - discountAmount;
    }
}
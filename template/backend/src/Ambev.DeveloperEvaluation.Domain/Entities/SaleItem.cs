using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem : BaseEntity
    {
        public Product Product { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal Discount { get; private set; }
        public decimal Total => CalculateTotal();
        public bool IsCancelled { get; private set; }

        public SaleItem(Product product, int quantity, decimal unitPrice)
        {
            if (quantity < 1)
                throw new ArgumentException("Quantity must be at least 1.");

            if (quantity > 20)
                throw new ArgumentException("Cannot sell more than 20 items of the same product.");

            Product = product ?? throw new ArgumentNullException(nameof(product));
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = CalculateDiscount(quantity);
            Id = Guid.NewGuid();
        }

        private decimal CalculateDiscount(int quantity)
        {
            if (quantity >= 10)
                return 0.20m;
            if (quantity >= 4)
                return 0.10m;
            return 0.0m;
        }

        private decimal CalculateTotal()
        {
            var gross = Quantity * UnitPrice;
            var discountAmount = gross * Discount;
            return gross - discountAmount;
        }

        public void Cancel()
        {
            if (IsCancelled)
                throw new InvalidOperationException("Item is already cancelled.");
            IsCancelled = true;
        }
    }
}
using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{

    public class Sale : BaseEntity
    {
        public string SaleNumber { get; private set; } = string.Empty;
        public DateTime SoldAt { get; private set; }
        public Customer Customer { get; private set; }
        public Branch Branch { get; private set; }
        public List<SaleItem> Items { get; private set; } = new();
        public decimal TotalAmount => CalculateTotalAmount();
        public bool IsCancelled { get; private set; }

        public List<string> DomainEvents { get; private set; } = new();

        public Sale(string saleNumber, Customer customer, Branch branch, List<SaleItem> items)
        {
            Id = Guid.NewGuid();
            SaleNumber = saleNumber;
            SoldAt = DateTime.UtcNow;
            Customer = customer ?? throw new ArgumentNullException(nameof(customer));
            Branch = branch ?? throw new ArgumentNullException(nameof(branch));
            Items = items ?? throw new ArgumentNullException(nameof(items));

            ValidateItems();
        }

        private void ValidateItems()
        {
            if (Items.Count == 0)
                throw new InvalidOperationException("Sale must contain at least one item.");
        }

        private decimal CalculateTotalAmount()
        {
            decimal total = 0;
            foreach (var item in Items)
            {
                total += item.Total;
            }
            return total;
        }

        private void AddDomainEvent(string eventName)
        {
            DomainEvents.Add(eventName);
        }

        public void Modify(List<SaleItem> newItems)
        {
            if (IsCancelled)
                throw new InvalidOperationException("Cannot modify a cancelled sale.");

            Items = newItems ?? throw new ArgumentNullException(nameof(newItems));
            ValidateItems();
            AddDomainEvent("SaleModified");
        }

        public void Cancel()
        {
            if (IsCancelled)
                throw new InvalidOperationException("Sale is already cancelled.");

            IsCancelled = true;
            foreach (var item in Items)
            {
                item.Cancel();
            }
        }
    }
}
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a shopping cart associated with a user and branch.
    /// Contains products, calculates totals, and tracks status changes.
    /// </summary>
    public class Sale
    {
        /// <summary>
        /// Gets or sets the unique identifier of the sale.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the user associated with the sale.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the branch associated with the sale.
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// Gets or sets the sale number, which uniquely identifies the sale within the system.
        /// Defaults to an empty string.
        /// </summary>
        public string Number { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date and time when the sale was created.
        /// Defaults to the current UTC time.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the date and time when the sale was last updated.
        /// Null if the sale has not been updated.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the sale was canceled.
        /// Null if the sale has not been canceled.
        /// </summary>
        public DateTime? CanceledAt { get; set; }

        /// <summary>
        /// Gets or sets the list of items included in the sale.
        /// </summary>
        public List<SaleItem> Items { get; set; } = null!;

        /// <summary>
        /// Gets or sets the user associated with the sale.
        /// </summary>
        public User User { get; set; } = null!;

        /// <summary>
        /// Gets or sets the branch associated with the sale.
        /// </summary>
        public Branch Branch { get; set; } = null!;

        /// <summary>
        /// Gets the total price of the sale after applying any discounts.
        /// </summary>
        public MonetaryValue TotalAmount { get; set; } = MonetaryValue.Zero;

        /// <summary>
        /// Marks the sale as canceled by updating its status and timestamps.
        /// </summary>
        public void SetAsCanceled()
        {
            UpdatedAt = DateTime.UtcNow;
            CanceledAt = DateTime.UtcNow;
        }

        public void SetAsUpdated()
        {
            UpdatedAt = DateTime.UtcNow;
        }

        public void Subtotal()
        {
            var subTotal = Items?
                .Where(x => !x.IsCanceled())
                .Sum(x => x.TotalAmount.Amount);

            TotalAmount = new MonetaryValue(subTotal ?? 0);
        }
    }

    /// <summary>
    /// Represents an item within a sale.
    /// Tracks pricing, discounts, and quantity.
    /// </summary>
    public class SaleItem
    {
        /// <summary>
        /// Gets or sets the unique identifier of the sale item.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the sale this item belongs to.
        /// </summary>
        public int SaleId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the product.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product in the sale.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit price of the product.
        /// Defaults to zero.
        /// </summary>
        public MonetaryValue UnitPrice { get; set; } = MonetaryValue.Zero;

        /// <summary>
        /// Gets or sets the discount applied to the product as a percentage.
        /// Defaults to zero.
        /// </summary>
        public PercentageValue DiscountPercent { get; set; } = PercentageValue.Zero;

        /// <summary>
        /// Gets or sets the total price of the product after applying the discount.
        /// Defaults to zero.
        /// </summary>
        public MonetaryValue TotalAmount { get; set; } = MonetaryValue.Zero;

        /// <summary>
        /// Gets or sets the date and time when the product was added to the sale.
        /// Defaults to the current UTC time.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the date and time when the product was last updated.
        /// Null if the product has not been updated.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the product was canceled.
        /// Null if the product has not been canceled.
        /// </summary>
        public DateTime? CanceledAt { get; set; }

        /// <summary>
        /// Gets or sets the sale this product belongs to.
        /// </summary>        
        public Sale Sale { get; set; } = null!;

        /// <summary>
        /// Gets or sets the product details.
        /// </summary>        
        public Product Product { get; set; } = null!;

        /// <summary>
        /// Marks the product as canceled by updating its status and timestamps.
        /// </summary>
        public void SetAsCanceled()
        {
            UpdatedAt = DateTime.UtcNow;
            CanceledAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Updates the product's last updated timestamp to the current UTC time.
        /// </summary>
        public void SetAsUpdated()
        {
            UpdatedAt = DateTime.UtcNow;
        }

        public bool IsCanceled()
        {
            return CanceledAt != null;
        }

        /// <summary>
        /// Applies a discount to the product and recalculates its pricing.
        /// </summary>
        /// <param name="discountPercentage">The discount percentage to apply.</param>
        public void ApplyDiscount(decimal discountPercentage)
        {
            DiscountPercent = new PercentageValue(discountPercentage);
            TotalAmount = UnitPrice.Subtract(UnitPrice.Multiply(discountPercentage / 100)).Multiply(Quantity);
        }
    }

}

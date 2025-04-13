using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a shopping cart associated with a user and branch.
    /// Contains products, calculates totals, and tracks status changes.
    /// </summary>
    public class Cart
    {
        /// <summary>
        /// Gets or sets the unique identifier of the cart.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the user associated with the cart.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the current status of the cart.
        /// Defaults to Active.
        /// </summary>
        public CartStatus Status { get; set; } = CartStatus.Active;

        /// <summary>
        /// Gets or sets the date and time when the cart was created.
        /// Defaults to the current UTC time.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the date and time when the cart was last updated.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the cart was canceled.
        /// </summary>
        public DateTime? CanceledAt { get; set; }

        /// <summary>
        /// Gets or sets the list of products in the cart.
        /// </summary>
        public List<CartItem> Items { get; set; } = null!;

        /// <summary>
        /// Gets or sets the user associated with the cart.
        /// </summary>
        public User User { get; set; } = null!;


        /// <summary>
        /// Marks the cart as deleted by updating its status and timestamps.
        /// </summary>
        public void SetAsCanceled()
        {
            UpdatedAt = DateTime.UtcNow;
            CanceledAt = DateTime.UtcNow;
            Status = CartStatus.Canceled;
        }

        public void SetAsConverted()
        {
            UpdatedAt = DateTime.UtcNow;
            Status = CartStatus.Converted;
        }

        public bool IsCancelled()
        {
            return Status == CartStatus.Canceled;
        }
    }

    /// <summary>
    /// Represents a product within a shopping cart.
    /// Tracks pricing, discounts, and quantity.
    /// </summary>
    public class CartItem
    {
        /// <summary>
        /// Gets or sets the unique identifier of the cart product.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the cart this product belongs to.
        /// </summary>
        public int CartId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the product.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product in the cart.
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
        /// Gets or sets the total amount of the product.
        /// Defaults to zero.
        /// </summary>
        public MonetaryValue TotalAmount { get; set; } = MonetaryValue.Zero;

        /// <summary>
        /// Gets or sets the date and time when the product was added to the cart.
        /// Defaults to the current UTC time.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the date and time when the product was last updated.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the product was canceled.
        /// </summary>
        public DateTime? CanceledAt { get; set; }

        /// <summary>
        /// Gets or sets the cart this product belongs to.
        /// </summary>
        public Cart Cart { get; set; } = null!;

        /// <summary>
        /// Gets or sets the product details.
        /// </summary>        
        public Product Product { get; set; } = null!;

        /// <summary>
        /// Applies a discount to the product and recalculates its pricing.
        /// </summary>
        /// <param name="discountPercentage">The discount percentage to apply.</param>
        public void ApplyDiscount(decimal discountPercentage)
        {
            DiscountPercent = new PercentageValue(discountPercentage);
            TotalAmount = UnitPrice.Subtract(UnitPrice.Multiply(discountPercentage / 100)).Multiply(Quantity);
        }

        public bool IsCancelled()
        {
            return CanceledAt != null;
        }

        /// <summary>
        /// Marks the product as deleted by updating its status and timestamps.
        /// </summary>
        public void SetAsCanceled()
        {
            UpdatedAt = DateTime.UtcNow;
            CanceledAt = DateTime.UtcNow;
        }


        /// <summary>
        /// Updates the product's last updated timestamp to the current UTC time.
        /// </summary>
        public void SetLastUpdated()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }

}

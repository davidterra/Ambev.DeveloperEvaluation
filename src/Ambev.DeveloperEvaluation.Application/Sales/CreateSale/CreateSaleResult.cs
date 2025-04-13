namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Represents the result of creating a sale.
/// This class encapsulates all relevant details about a sale, including its identifier, associated user and branch, items, timestamps, and total amount.
/// It also provides mechanisms to track the sale's lifecycle, such as cancellation.
/// </summary>
public class CreateSaleResult
{
    /// <summary>
    /// Gets or sets the sale number, which uniquely identifies the sale within the system.
    /// Defaults to an empty string if not explicitly set.
    /// </summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date and time when the sale was created.
    /// Defaults to the current UTC time at the moment of creation.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the unique identifier of the user associated with the sale.
    /// This links the sale to a specific user in the system.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the branch associated with the sale.
    /// This links the sale to a specific branch where it was created.
    /// </summary>
    public int BranchId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the sale.
    /// This identifier is auto-generated and serves as the primary key for the sale.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the list of items included in the sale.
    /// Each item contains details such as product, quantity, and pricing.
    /// </summary>
    public List<CreateSaleItemResult> Items { get; set; } = null!;

    /// <summary>
    /// Gets the total price of the sale after applying any discounts.
    /// Defaults to zero if no items or discounts are present.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the sale was canceled.
    /// Null if the sale has not been canceled.
    /// </summary>
    public DateTime? CanceledAt { get; set; }
}

/// <summary>
/// Represents the result of creating an item within a sale.
/// This class tracks details such as the product, quantity, pricing, discounts, and timestamps.
/// It also provides mechanisms to track the item's lifecycle, such as updates and cancellations.
/// </summary>
public class CreateSaleItemResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the sale item.
    /// This identifier is auto-generated and serves as the primary key for the sale item.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the sale this item belongs to.
    /// This links the item to a specific sale.
    /// </summary>
    public int SaleId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the product.
    /// This links the item to a specific product in the system.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the product in the sale.
    /// Represents the number of units of the product included in the sale.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the unit price of the product.
    /// Defaults to zero if not explicitly set.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the discount applied to the product as a percentage.
    /// Defaults to zero if no discount is applied.
    /// </summary>
    public decimal DiscountPercent { get; set; }

    /// <summary>
    /// Gets or sets the total price of the product after applying the discount.
    /// Defaults to zero if no discount is applied.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the product was added to the sale.
    /// Defaults to the current UTC time at the moment of creation.
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
}

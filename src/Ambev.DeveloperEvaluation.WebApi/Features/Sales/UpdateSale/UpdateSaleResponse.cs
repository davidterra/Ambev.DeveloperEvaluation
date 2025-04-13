namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;


/// <summary>
/// Represents the response for updating a sale.
/// Contains details about the sale, including its identifier, associated user and branch, items, and timestamps.
/// Provides methods to manage the sale's lifecycle, such as marking it as canceled.
/// </summary>
public class UpdateSaleResponse
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
    public List<UpdateSaleItemResponse> Items { get; set; } = null!;

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
/// Represents an item within a sale.
/// Tracks pricing, discounts, and quantity.
/// </summary>
public class UpdateSaleItemResponse
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
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the discount applied to the product as a percentage.
    /// Defaults to zero.
    /// </summary>
    public decimal DiscountPercent { get; set; }

    /// <summary>
    /// Gets or sets the total price of the product after applying the discount.
    /// Defaults to zero.
    /// </summary>
    public decimal TotalAmount { get; set; }

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
}

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCartItem;

/// <summary>
/// Represents the result of updating a cart item, including details about the item, pricing, and discounts.
/// </summary>
public class UpdateCartItemResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the cart item.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the product associated with the cart item.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the product in the cart.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the unit price of the product.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the discount amount applied to the product.
    /// </summary>
    public decimal DiscountAmount { get; set; }

    /// <summary>
    /// Gets or sets the unit price of the product after applying the discount.
    /// </summary>
    public decimal TotalAmount { get; set; }

}

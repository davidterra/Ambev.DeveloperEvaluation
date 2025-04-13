namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCartItem;

/// <summary>
/// Represents the response for updating a cart item.
/// </summary>
public class UpdateCartItemResponse
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
    /// Gets or sets the quantity of the product in the cart item.
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

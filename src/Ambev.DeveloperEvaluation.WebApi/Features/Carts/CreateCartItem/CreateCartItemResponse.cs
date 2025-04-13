namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCartItem;

/// <summary>
/// Represents the response for creating a cart item.
/// </summary>
public class CreateCartItemResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the cart item.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the user associated with the cart item.
    /// </summary>
    public Guid UserId { get; set; }


    /// <summary>
    /// Gets or sets the product details associated with the cart item.
    /// </summary>
    public CreateItemCartProductResponse Product { get; set; } = null!;
}

/// <summary>
/// Represents the product details in a cart item.
/// </summary>
public class CreateItemCartProductResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the cart product.
    /// </summary>
    public int Id { get; set; }

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
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the discount amount applied to the product.
    /// </summary>
    public decimal DiscountAmount { get; set; }

    /// <summary>
    /// Gets or sets the total price of the product after applying the discount.
    /// </summary>
    public decimal TotalAmount { get; set; }
}

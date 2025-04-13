namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCartItem;

/// <summary>
/// Represents a request to create a cart item for a specific user and branch.
/// </summary>
public class CreateCartItemRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the user.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the product details for the cart item.
    /// </summary>
    public CreateItemCartProductRequest Product { get; set; } = null!;
}

/// <summary>
/// Represents the product details for a cart item, including product ID and quantity.
/// </summary>
public class CreateItemCartProductRequest
{
    /// <summary>
    /// Gets or sets the identifier of the product.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the product to be added to the cart.
    /// </summary>
    public int Quantity { get; set; }
}

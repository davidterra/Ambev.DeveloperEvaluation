namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCartItem;

/// <summary>
/// Represents a request to update an item in the cart.
/// </summary>
public class UpdateCartItemRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the cart item to be updated.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the new quantity for the cart item.
    /// </summary>
    public int Quantity { get; set; }
}

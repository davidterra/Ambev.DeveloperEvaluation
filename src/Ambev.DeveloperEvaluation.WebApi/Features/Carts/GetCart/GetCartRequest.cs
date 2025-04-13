namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;

/// <summary>
/// Represents a request to retrieve a cart by its unique identifier.
/// </summary>
public class GetCartRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the cart.
    /// </summary>
    public int Id { get; set; }
}

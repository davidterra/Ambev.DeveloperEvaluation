namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteCartItem;

/// <summary>
/// Request model for deleting a product from the cart.
/// </summary>
public class DeleteCartItemRequest
{
    /// <summary>
    /// The unique identifier of the product to delete from the cart.
    /// </summary>
    public int Id { get; set; }
}

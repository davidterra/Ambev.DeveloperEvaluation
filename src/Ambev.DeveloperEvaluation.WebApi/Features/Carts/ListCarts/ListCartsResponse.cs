using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.ListCarts;

/// <summary>
/// Represents the response for listing carts, including cart details, status, and associated products.
/// </summary>
public class ListCartsResponse
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
    /// Gets or sets the date and time when the cart was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the current status of the cart.
    /// </summary>
    public CartStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the list of products associated with the cart.
    /// </summary>
    public List<ListCartsItemResponse> Items { get; set; } = [];

}    

/// <summary>
/// Represents the response for a product in a cart, including pricing and status details.
/// </summary>
public class ListCartsItemResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the cart product entry.
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
    /// Gets or sets the discount amount applied to the product.
    /// </summary>
    public decimal DiscountAmount { get; set; }

    /// <summary>
    /// Gets or sets the unit price of the product after applying the discount.
    /// </summary>
    public decimal TotalAmount { get; set; }
    
    /// <summary>
    /// Gets or sets the date and time when the product was added to the cart.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the product details were last updated, if applicable.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the product was canceled, if applicable.
    /// </summary>
    public DateTime? CanceledAt { get; set; }
}

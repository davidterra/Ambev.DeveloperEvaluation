using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;

/// <summary>
/// Represents the response for retrieving a cart, including its details and associated products.
/// </summary>
public class GetCartResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the cart.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the user who owns the cart.
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
    public List<GetCartItemResponse> Items { get; set; } = [];
    
    /// <summary>
    /// Gets or sets the total price of the cart after applying discounts.
    /// </summary>
    public decimal TotalAmount { get; set; }
}

/// <summary>
/// Represents the response for a product within a cart, including pricing and status details.
/// </summary>
public class GetCartItemResponse
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
    /// Gets or sets the total price of the product after applying discounts.
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

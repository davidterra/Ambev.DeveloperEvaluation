namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCartItem;

/// <summary>
/// Represents the result of creating a cart item, including its details and associated product information.
/// </summary>
public class CreateCartItemResult
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
    public CreateItemCartProductResult Product { get; set; } = null!;
}

/// <summary>
/// Represents the details of a product in a cart item, including pricing and quantity information.
/// </summary>
public class CreateItemCartProductResult
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
    public decimal DiscountPercent { get; set; }

    /// <summary>
    /// Gets or sets the unit price of the product after applying the discount.
    /// </summary>
    public decimal TotalAmount { get; set; }

}

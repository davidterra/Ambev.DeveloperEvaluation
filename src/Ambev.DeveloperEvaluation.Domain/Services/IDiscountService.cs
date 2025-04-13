using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services;

/// <summary>
/// Provides methods to apply discounts to cart products.
/// </summary>
public interface IDiscountService
{
    /// <summary>
    /// Applies a discount to the specified cart product.
    /// Updates the product's price and totals based on the discount logic.
    /// </summary>
    /// <param name="product">The cart product to which the discount will be applied.</param>
    void ApplyDiscount(CartItem product);
}

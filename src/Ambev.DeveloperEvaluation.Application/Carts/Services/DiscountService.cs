using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Domain.Specifications.Discounts;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentValidation;
using FluentValidation.Results;

namespace Ambev.DeveloperEvaluation.Application.Carts.Services;

/// <summary>
/// Service responsible for applying discounts to cart products based on specific discount rules and specifications.
/// </summary>
public class DiscountService : IDiscountService
{
    /// <summary>
    /// Applies the appropriate discount to a given cart product.
    /// Resets any existing discounts before applying new ones.
    /// Throws a validation exception if the product is not eligible for any discount.
    /// </summary>
    /// <param name="product">The cart product to which the discount will be applied.</param>
    public void ApplyDiscount(CartItem product)
    {
        ResetProductDiscounts(product);

        if (new TenPercentDiscountSpecification().IsSatisfiedBy(product))
        {
            product.ApplyDiscount(10);
        }
        else if (new TwentyPercentDiscountSpecification().IsSatisfiedBy(product))
        {
            product.ApplyDiscount(20);
        }
        else
        {
            var validationFailure = ValidateDiscountEligibility(product);
            if (validationFailure != null)
            {
                throw new ValidationException([validationFailure]);
            }
        }
    }

    /// <summary>
    /// Resets all discount-related properties of a cart product to their default values.
    /// </summary>
    /// <param name="product">The cart product whose discount properties will be reset.</param>
    private static void ResetProductDiscounts(CartItem product)
    {
        product.DiscountPercent = PercentageValue.Zero;
        product.TotalAmount = MonetaryValue.Zero;
    }

    /// <summary>
    /// Validates the eligibility of a cart product for discounts based on specific rules.
    /// Returns a validation failure if the product violates any rules.
    /// </summary>
    /// <param name="product">The cart product to validate.</param>
    /// <returns>
    /// A <see cref="ValidationFailure"/> object if the product is not eligible for discounts; otherwise, null.
    /// </returns>
    private static ValidationFailure ValidateDiscountEligibility(CartItem product)
    {
        if (new NoDiscountSpecification().IsSatisfiedBy(product))
        {
            return new ValidationFailure("DiscountPercent", "No discounts allowed for quantities below 4 items") { ErrorCode = "Invalid input data" };
        }

        if (new QuantityLimitSpecification().IsSatisfiedBy(product))
        {
            return new ValidationFailure("DiscountPercent", "It's not possible to sell above 20 identical items") { ErrorCode = "Invalid input data" };
        }

        return null!;
    }
}

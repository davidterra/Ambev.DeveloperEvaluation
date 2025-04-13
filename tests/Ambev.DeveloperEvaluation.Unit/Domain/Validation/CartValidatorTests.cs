using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

/// <summary>
/// Contains unit tests for the <see cref="CartValidator"/> class.
/// </summary>
public class CartValidatorTests
{
    private readonly CartValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="CartValidatorTests"/> class.
    /// </summary>
    public CartValidatorTests()
    {
        _validator = new CartValidator();
    }

    /// <summary>
    /// Tests that a valid cart passes validation.
    /// </summary>
    [Fact(DisplayName = "Given valid cart When validating Then passes validation")]
    public void Validate_ValidCart_PassesValidation()
    {
        // Given
        var cart = new Cart
        {
            UserId = Guid.NewGuid(),
            Status = CartStatus.Active,
            Items = new List<CartItem>
            {
                new CartItem { ProductId = 1, Quantity = 2 }
            }
        };

        // When
        var result = _validator.TestValidate(cart);

        // Then
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Tests that a cart with an empty user ID fails validation.
    /// </summary>
    [Fact(DisplayName = "Given empty user ID When validating Then fails validation")]
    public void Validate_EmptyUserId_FailsValidation()
    {
        // Given
        var cart = new Cart
        {
            UserId = Guid.Empty, // Invalid            
            Status = CartStatus.Active,
            Items = new List<CartItem>
            {
                new CartItem { ProductId = 1, Quantity = 2 }
            }
        };

        // When
        var result = _validator.TestValidate(cart);

        // Then
        result.ShouldHaveValidationErrorFor(c => c.UserId)
            .WithErrorMessage("User ID is required.");
    }

    /// <summary>
    /// Tests that a cart with an invalid status fails validation.
    /// </summary>
    [Fact(DisplayName = "Given invalid cart status When validating Then fails validation")]
    public void Validate_InvalidCartStatus_FailsValidation()
    {
        // Given
        var cart = new Cart
        {
            UserId = Guid.NewGuid(),
            Status = CartStatus.Unknown, // Invalid
            Items = new List<CartItem>
            {
                new CartItem { ProductId = 1, Quantity = 2 }
            }
        };

        // When
        var result = _validator.TestValidate(cart);

        // Then
        result.ShouldHaveValidationErrorFor(c => c.Status)
            .WithErrorMessage("Invalid cart status.");
    }

    /// <summary>
    /// Tests that a cart with null products fails validation.
    /// </summary>
    [Fact(DisplayName = "Given null products When validating Then fails validation")]
    public void Validate_NullProducts_FailsValidation()
    {
        // Given
        var cart = new Cart
        {
            UserId = Guid.NewGuid(),
            Status = CartStatus.Active,
            Items = null! // Invalid
        };

        // When
        var result = _validator.TestValidate(cart);

        // Then
        result.ShouldHaveValidationErrorFor(c => c.Items)
            .WithErrorMessage("Cart must contain at least one product.");
    }

    /// <summary>
    /// Tests that a cart with a product having zero quantity fails validation.
    /// </summary>
    [Fact(DisplayName = "Given product with zero quantity When validating Then fails validation")]
    public void Validate_ProductWithZeroQuantity_FailsValidation()
    {
        // Given
        var cart = new Cart
        {
            UserId = Guid.NewGuid(),
            Status = CartStatus.Active,
            Items = new List<CartItem>
            {
                new CartItem { ProductId = 1, Quantity = 0 } // Invalid
            }
        };

        // When
        var result = _validator.TestValidate(cart);

        // Then
        result.ShouldHaveValidationErrorFor(c => c.Items)
            .WithErrorMessage("All products in the cart must have a quantity greater than zero.");
    }
}

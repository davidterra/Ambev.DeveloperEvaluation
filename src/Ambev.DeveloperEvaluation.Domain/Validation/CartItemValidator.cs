using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class CartItemValidator : AbstractValidator<CartItem>
    {
        public CartItemValidator()
        {
            RuleFor(cartProduct => cartProduct.ProductId)
                .GreaterThan(0)
                .WithMessage("Product ID is required.");

            RuleFor(cartProduct => cartProduct.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than zero.");

            RuleFor(cartProduct => cartProduct.UnitPrice)
                .NotNull()
                .WithMessage("Unit price is required.")
                .Must(price => price?.Amount > 0)
                .WithMessage("Unit price must be greater than zero.");

        }
    }

}

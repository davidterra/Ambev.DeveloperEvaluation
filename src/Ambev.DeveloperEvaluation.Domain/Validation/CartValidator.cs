using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class CartValidator : AbstractValidator<Cart>
    {
        public CartValidator()
        {
            RuleFor(cart => cart.UserId)
                .NotEmpty()
                .Must(userId => userId != Guid.Empty)
                .WithMessage("User ID is required.");

            RuleFor(cart => cart.Status)
                .IsInEnum()
                .Must(status => status != CartStatus.Unknown)
                .WithMessage("Invalid cart status.");

            RuleFor(cart => cart.Items)
                .NotNull().WithMessage("Cart must contain at least one product.")
                .Must(products => products != null && products.All(p => p.Quantity > 0))
                .WithMessage("All products in the cart must have a quantity greater than zero.");
        }
    }
}

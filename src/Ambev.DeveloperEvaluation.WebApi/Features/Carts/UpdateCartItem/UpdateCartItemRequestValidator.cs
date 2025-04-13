using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCartItem;

/// <summary>
/// Validator for the <see cref="UpdateCartItemRequest"/> class.
/// Ensures that the request contains valid data for updating a cart item.
/// </summary>
public class UpdateCartItemRequestValidator : AbstractValidator<UpdateCartItemRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateCartItemRequestValidator"/> class.
    /// Defines validation rules for the <see cref="UpdateCartItemRequest"/> properties.
    /// </summary>
    public UpdateCartItemRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Id must be greater than 0");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than 0");
    }
}

using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCartItem;


/// <summary>
/// Validator for the CreateCartItemRequest class. Ensures that the request contains valid data.
/// </summary>
public class CreateCartItemRequestValidator : AbstractValidator<CreateCartItemRequest>
{
    /// <summary>
    /// Initializes a new instance of the CreateCartItemRequestValidator class.
    /// Defines validation rules for the CreateCartItemRequest properties.
    /// </summary>
    public CreateCartItemRequestValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId is required.")
            .Must(x => Guid.TryParse(x.ToString(), out _))
            .WithMessage("UserId must be a valid GUID.");

        RuleFor(x => x.Product)
            .NotNull()
            .WithMessage("Product is required.")
            .SetValidator(new CreateCartProductRequestValidator());
    }
}

/// <summary>
/// Validator for the CreateItemCartProductRequest class. Ensures that the product details are valid.
/// </summary>
public class CreateCartProductRequestValidator : AbstractValidator<CreateItemCartProductRequest>
{
    /// <summary>
    /// Initializes a new instance of the CreateCartProductRequestValidator class.
    /// Defines validation rules for the CreateItemCartProductRequest properties.
    /// </summary>
    public CreateCartProductRequestValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0)
            .WithMessage("ProductId must be greater than zero.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than zero.");
    }
}

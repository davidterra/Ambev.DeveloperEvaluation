using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCartItem;

/// <summary>
/// Validator for the CreateCartItemCommand class.
/// Ensures that the UserId is not empty and is a valid GUID, BranchId is greater than zero,
/// and the Product property is not null and adheres to the rules defined in CreateCartItemProductCommandValidator.
/// </summary>
public class CreateCartItemCommandValidator : AbstractValidator<CreateCartItemCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateCartItemCommandValidator class.
    /// Defines validation rules for the CreateCartItemCommand properties.
    /// </summary>
    public CreateCartItemCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId is required.")
            .Must(x => Guid.TryParse(x.ToString(), out _))
            .WithMessage("UserId must be a valid GUID.");

        RuleFor(x => x.Product)
            .NotNull()
            .WithMessage("Product is required.")
            .SetValidator(new CreateCartItemProductCommandValidator());
    }
}

/// <summary>
/// Validator for the CreateCartItemProductCommand class.
/// Ensures that the ProductId is greater than zero and the Quantity is greater than zero.
/// </summary>
public class CreateCartItemProductCommandValidator : AbstractValidator<CreateCartItemProductCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateCartItemProductCommandValidator class.
    /// Defines validation rules for the CreateCartItemProductCommand properties.
    /// </summary>
    public CreateCartItemProductCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0)
            .WithMessage("ProductId must be greater than zero.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than zero.");
    }
}

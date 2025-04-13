using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;

/// <summary>
/// Validator for DeleteCartCommand
/// </summary>
public class DeleteCartValidator : AbstractValidator<DeleteCartCommand>
{
    /// <summary>
    /// Initializes validation rules for DeleteCartCommand
    /// </summary>
    public DeleteCartValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Id must be greater than zero.");
    }
}

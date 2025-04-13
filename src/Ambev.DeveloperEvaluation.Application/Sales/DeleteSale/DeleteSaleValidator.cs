using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

/// <summary>
/// Validator for DeleteSaleCommand, ensuring that the command contains valid data before processing.
/// </summary>
public class DeleteSaleValidator : AbstractValidator<DeleteSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteSaleValidator"/> class.
    /// Defines validation rules for the DeleteSaleCommand.
    /// Ensures that the Id property is greater than 0.
    /// </summary>
    public DeleteSaleValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Sale ID is required and must be greater than 0.");
    }
}

using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSaleItem;

/// <summary>
/// Validator for the <see cref="DeleteSaleItemCommand"/> class.
/// Ensures that the command contains valid data before processing, such as verifying that required fields are populated and meet specific criteria.
/// </summary>
public class DeleteSaleItemValidator : AbstractValidator<DeleteSaleItemCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteSaleItemValidator"/> class.
    /// Defines validation rules for the <see cref="DeleteSaleItemCommand"/> properties.
    /// Ensures that the <see cref="DeleteSaleItemCommand.Id"/> and <see cref="DeleteSaleItemCommand.ItemId"/> properties are greater than zero.
    /// </summary>
    public DeleteSaleItemValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("User ID is required");

        RuleFor(x => x.ItemId)
            .GreaterThan(0)
            .WithMessage("Item ID is required");
    }
}

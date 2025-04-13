using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSaleItem;

/// <summary>
/// Validator for DeleteSaleItemRequest, ensuring that the request contains valid data for deleting a sale item.
/// </summary>
public class DeleteSaleItemRequestValidator : AbstractValidator<DeleteSaleItemRequest>
{
    /// <summary>
    /// Initializes validation rules for DeleteSaleItemRequest.
    /// Ensures that the Id and ItemId properties are greater than zero, indicating valid identifiers.
    /// </summary>
    public DeleteSaleItemRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("User ID is required");

        RuleFor(x => x.ItemId)
            .GreaterThan(0)
            .WithMessage("Item ID is required");
    }
}

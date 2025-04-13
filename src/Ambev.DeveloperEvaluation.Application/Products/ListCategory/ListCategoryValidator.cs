using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.ListCategory;

/// <summary>
/// Validator for the <see cref="ListCategoryCommand"/> class.
/// Ensures that the Category property is not empty and does not exceed the maximum length of 100 characters.
/// </summary>
public class ListCategoryValidator : AbstractValidator<ListCategoryCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ListCategoryValidator"/> class.
    /// Defines validation rules for the <see cref="ListCategoryCommand"/> properties.
    /// </summary>
    public ListCategoryValidator()
    {
        RuleFor(x => x.Category)
            .NotEmpty()
            .WithMessage("Category is required.")
            .MaximumLength(100)
            .WithMessage("Category must not exceed 100 characters.");
    }
}

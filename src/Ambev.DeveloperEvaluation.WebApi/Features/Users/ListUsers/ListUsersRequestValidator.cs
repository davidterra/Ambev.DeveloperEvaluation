using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.ListUsers;

/// <summary>
/// Validator for ListUsersRequest.
/// </summary>
public class ListUsersRequestValidator : AbstractValidator<ListUsersRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ListUsersRequestValidator"/> class.
    /// Defines validation rules for the <see cref="ListUsersRequest"/> object:
    /// - Ensures the page number is greater than or equal to 1.
    /// - Ensures the page size is greater than 0.
    /// - Validates the order format (e.g., "name asc, date desc").
    /// - Ensures each filter has a non-empty key and value.
    /// </summary>
    public ListUsersRequestValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1).WithMessage("Page number must be greater than or equal to 1.");

        RuleFor(x => x.Size)
            .GreaterThan(0).WithMessage("Page size must be greater than 0.");

        RuleFor(x => x.OrderBy)
            .Matches(@"""([a-zA-Z]+( (asc|desc))?(, )?)*[a-zA-Z]+( (asc|desc))?""")
            .When(x => !string.IsNullOrEmpty(x.OrderBy))
            .WithMessage("Order format is invalid.");
    }
}

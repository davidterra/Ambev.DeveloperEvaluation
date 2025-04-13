using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers;

/// <summary>
/// Command to list users with optional ordering and filtering.
/// </summary>
public record ListUsersCommand : IRequest<IQueryable<ListUsersResult>>
{
    /// <summary>
    /// Specifies the property to order the results by.
    /// </summary>
    public string OrderBy { get; set; } = string.Empty;

    /// <summary>
    /// A dictionary of filters to apply to the user list query.
    /// The key represents the field name, and the value represents the filter value.
    /// </summary>
    public Dictionary<string, string>? Filters { get; set; }
}

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.ListUsers;

/// <summary>
/// Represents a request to list users with pagination, sorting, and filtering options.
/// </summary>
public class ListUsersRequest
{
    /// <summary>
    /// Gets or sets the page number for pagination. Default is 1.
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Gets or sets the number of items per page for pagination. Default is 10.
    /// </summary>
    public int Size { get; set; } = 10;

    /// <summary>
    /// Gets or sets the order in which to sort the users. 
    /// Example: "Name ASC" or "CreatedDate DESC".
    /// </summary>
    public string OrderBy { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the filters to apply to the user list.
    /// The key represents the field to filter by, and the value represents the filter value.
    /// Example: { "Name": "John", "Role": "Admin" }.
    /// </summary>
    public Dictionary<string, string>? Filters { get; set; }
}


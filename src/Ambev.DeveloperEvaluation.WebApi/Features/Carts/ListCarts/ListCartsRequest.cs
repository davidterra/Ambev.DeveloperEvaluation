namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.ListCarts;

/// <summary>
/// Represents a request to list carts with pagination, sorting, and filtering options.
/// </summary>
public class ListCartsRequest
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
    /// Gets or sets the field by which to sort the carts. Default is an empty string, meaning no specific sorting.
    /// </summary>
    public string OrderBy { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the filters to apply to the cart list. Filters are represented as key-value pairs.
    /// </summary>
    public Dictionary<string, string>? Filters { get; set; }
}

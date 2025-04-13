using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.ListCarts;

/// <summary>
/// Represents a request to list carts with pagination, sorting, and filtering options.
/// </summary>
public class ListCartsCommand : IRequest<IQueryable<ListCartsResult>>
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
    /// Gets or sets the order in which to sort the carts. 
    /// This should be a valid property name of <see cref="ListCartsResult"/> to sort by.
    /// </summary>
    public string OrderBy { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the filters to apply to the cart list.
    /// The key represents the property name of <see cref="ListCartsResult"/> to filter by, 
    /// and the value represents the filter criteria.
    /// </summary>
    public Dictionary<string, string>? Filters { get; set; }
}

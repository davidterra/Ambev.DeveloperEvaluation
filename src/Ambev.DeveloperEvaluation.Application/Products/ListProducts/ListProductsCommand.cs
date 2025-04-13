using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts;

/// <summary>
/// Represents a request to list products with pagination, sorting, and filtering options.
/// </summary>
public class ListProductsCommand : IRequest<IQueryable<ListProductsResult>>
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
    /// Gets or sets the order in which to sort the products.
    /// The value should be a valid property name of the product, optionally followed by "asc" or "desc" to specify the sort direction.
    /// Example: "Title asc" or "Price desc".
    /// </summary>
    public string OrderBy { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the filters to apply to the product list.
    /// The dictionary keys represent the property names to filter by, and the values represent the filter criteria.
    /// Example: { "Category", "Electronics" } to filter products in the "Electronics" category.
    /// </summary>
    public Dictionary<string, string>? Filters { get; set; }
}


namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListCategory;

/// <summary>
/// Represents a request to list product categories with pagination, sorting, and optional filters.
/// </summary>
public class ListCategoryRequest
{
    /// <summary>
    /// Gets or sets the category name to filter the products.
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the page number for pagination.
    /// </summary>
    public int Page { get; set; }

    /// <summary>
    /// Gets or sets the number of items per page for pagination.
    /// </summary>
    public int Size { get; set; }

    /// <summary>
    /// Gets or sets the field by which the results should be ordered.
    /// </summary>
    public string OrderBy { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets additional filters as key-value pairs to refine the results.
    /// </summary>
    public Dictionary<string, string>? Filters { get; set; }
}

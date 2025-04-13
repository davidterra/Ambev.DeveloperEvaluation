namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProducts;

/// <summary>
/// Represents a request to list products with pagination, sorting, and filtering options.
/// </summary>
public class ListProductsRequest
{
    /// <summary>
    /// Gets or sets the page number for pagination. Default is 1.
    /// </summary>
    /// <remarks>
    /// This property determines which page of the product list to retrieve.
    /// </remarks>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Gets or sets the number of items per page for pagination. Default is 10.
    /// </summary>
    /// <remarks>
    /// This property specifies the maximum number of products to include in a single page.
    /// </remarks>
    public int Size { get; set; } = 10;

    /// <summary>
    /// Gets or sets the order in which to sort the products.
    /// </summary>
    /// <remarks>
    /// The value should specify the field name and optionally the sort direction (e.g., "Name ASC" or "Price DESC").
    /// </remarks>
    public string OrderBy { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the filters to apply to the product list.
    /// </summary>
    /// <remarks>
    /// Filters are represented as key-value pairs where the key is the field name and the value is the filter criteria.
    /// </remarks>
    public Dictionary<string, string>? Filters { get; set; }
}

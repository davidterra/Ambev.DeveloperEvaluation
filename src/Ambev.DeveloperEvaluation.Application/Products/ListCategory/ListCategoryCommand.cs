using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListCategory;

/// <summary>
/// Command to list products by category with optional filters and sorting.
/// </summary>
public record ListCategoryCommand : IRequest<IQueryable<ListCategoryResult>>
{
    /// <summary>
    /// Gets the category of the products to be listed.
    /// </summary>
    public string Category { get; }

    /// <summary>
    /// Gets the property by which the results should be ordered.
    /// </summary>
    public string OrderBy { get; }

    /// <summary>
    /// Gets the optional filters to apply to the product list.
    /// </summary>
    public Dictionary<string, string>? Filters { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ListCategoryCommand"/> class.
    /// </summary>
    /// <param name="category">The category of the products to be listed.</param>
    /// <param name="orderBy">The property by which the results should be ordered.</param>
    /// <param name="filters">Optional filters to apply to the product list.</param>
    public ListCategoryCommand(string category, string orderBy, Dictionary<string, string>? filters)
    {
        Category = category;
        OrderBy = orderBy;
        Filters = filters;
    }
}

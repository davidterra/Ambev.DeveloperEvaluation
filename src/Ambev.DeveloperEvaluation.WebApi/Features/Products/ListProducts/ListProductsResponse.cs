namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProducts;

/// <summary>
/// API response model for listing products in the system.
/// </summary>
public class ListProductsResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the product.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the product.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the price of the product in the specified currency.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the detailed description of the product.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category to which the product belongs.
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the URL of the product's image.
    /// </summary>
    public string Image { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the rating details of the product, including rate value and count.
    /// </summary>
    public ListProductsRatingResponse Rating { get; set; } = new();

    /// <summary>
    /// Gets or sets the UTC date and time when the product was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the UTC date and time when the product was last updated, if applicable.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}

/// <summary>
/// Represents the rating details of a product, including the average rate and the total count of ratings.
/// </summary>
public class ListProductsRatingResponse
{
    /// <summary>
    /// Gets or sets the average rate value of the product.
    /// </summary>
    public decimal Rate { get; set; }

    /// <summary>
    /// Gets or sets the total number of ratings received by the product.
    /// </summary>
    public int Count { get; set; }
}

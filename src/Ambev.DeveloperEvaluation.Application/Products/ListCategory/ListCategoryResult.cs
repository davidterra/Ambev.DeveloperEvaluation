namespace Ambev.DeveloperEvaluation.Application.Products.ListCategory;

/// <summary>
/// Response model for ListCategory operation, representing the details of a product.
/// </summary>
public class ListCategoryResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the product.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the product, which is a short descriptive name.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the price of the product in decimal format.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the description of the product, providing additional details.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category of the product, indicating its classification.
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the image URL of the product, used for visual representation.
    /// </summary>
    public string Image { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the rating of the product, including rate value and count of ratings.
    /// </summary>
    public ListCategoryRatingResult Rating { get; set; } = new();

    /// <summary>
    /// Gets or sets the creation date of the product in UTC format.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated date of the product, if applicable.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}

/// <summary>
/// Represents the rating details of a product, including the rate value and the count of ratings.
/// </summary>
public class ListCategoryRatingResult
{
    /// <summary>
    /// Gets or sets the rate value of the product, representing the average rating.
    /// </summary>
    public decimal Rate { get; set; }

    /// <summary>
    /// Gets or sets the count of ratings for the product, indicating the number of reviews.
    /// </summary>
    public int Count { get; set; }
}


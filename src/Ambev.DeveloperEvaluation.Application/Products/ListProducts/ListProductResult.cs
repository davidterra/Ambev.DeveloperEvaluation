namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts;

/// <summary>
/// Response model for GetProducts operation.
/// This class represents the details of a product, including its metadata, pricing, and rating information.
/// </summary>
public class ListProductsResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the product.
    /// This is a primary key used to identify the product in the system.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the product.
    /// The title is a short, descriptive name for the product.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the price of the product.
    /// The price is represented as a decimal value in the system's currency.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the description of the product.
    /// The description provides detailed information about the product.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category of the product.
    /// The category groups the product into a specific classification.
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the image URL of the product.
    /// The image URL points to a visual representation of the product.
    /// </summary>
    public string Image { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the rating of the product.
    /// The rating includes the average rate and the total count of ratings.
    /// </summary>
    public ListProductsRatingResult Rating { get; set; } = new();

    /// <summary>
    /// Gets or sets the creation date of the product.
    /// This property indicates when the product was first added to the system.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated date of the product.
    /// This property indicates the most recent modification date of the product, if any.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}

/// <summary>
/// Represents the rating details of a product.
/// This class encapsulates the average rating value and the total number of ratings.
/// </summary>
public class ListProductsRatingResult
{
    /// <summary>
    /// Gets or sets the rate value of the product.
    /// The rate is an average value calculated from all user ratings.
    /// </summary>
    public decimal Rate { get; set; }

    /// <summary>
    /// Gets or sets the count of ratings for the product.
    /// The count represents the total number of users who rated the product.
    /// </summary>
    public int Count { get; set; }
}


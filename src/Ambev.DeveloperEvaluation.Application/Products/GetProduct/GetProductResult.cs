namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

/// <summary>
/// Response model for GetProduct operation.
/// This class encapsulates the details of a product retrieved from the system.
/// </summary>
public class GetProductResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the product.
    /// This is a primary key used to identify the product in the database.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the product.
    /// Represents the name or label of the product.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the price of the product.
    /// Indicates the cost of the product in the specified currency.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the description of the product.
    /// Provides detailed information about the product.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category of the product.
    /// Specifies the classification or grouping of the product.
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the image URL of the product.
    /// Contains the link to the product's image resource.
    /// </summary>
    public string Image { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the rating of the product.
    /// Includes the rate value and the count of ratings for the product.
    /// </summary>
    public GetProductRatingResult Rating { get; set; } = new();

    /// <summary>
    /// Gets or sets the creation date of the product.
    /// Represents the timestamp when the product was added to the system.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated date of the product.
    /// Represents the timestamp of the most recent update to the product details.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}

/// <summary>
/// Represents the rating details of a product.
/// This class provides information about the product's rating and the number of ratings received.
/// </summary>
public class GetProductRatingResult
{
    /// <summary>
    /// Gets or sets the rate value of the product.
    /// Indicates the average rating score of the product.
    /// </summary>
    public decimal Rate { get; set; }

    /// <summary>
    /// Gets or sets the count of ratings for the product.
    /// Represents the total number of ratings submitted for the product.
    /// </summary>
    public int Count { get; set; }
}

